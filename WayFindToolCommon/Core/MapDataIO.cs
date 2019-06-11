using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using WayFindToolCommon.Model;

namespace WayFindToolCommon.Core
{
    class MapDataIO
    {
        private object _lock = new object();

        public MapDataIO()
        {

        }

        public MapDataModel Load(string file)
        {
            MapDataModel result = new MapDataModel();
            FileInfo fileInfo = new FileInfo(file);

            if (fileInfo.Exists)
            {
                lock (_lock)
                {
                    try
                    {
                        MapDataModel mapDataList = new MapDataModel();
                        using (Stream stream = fileInfo.OpenRead())
                        using (StreamReader streamReader = new StreamReader(stream))
                        using (JsonReader jsonReader = new JsonTextReader(streamReader))
                        {
                            JsonSerializer jsonSerializer = new JsonSerializer();
                            mapDataList = jsonSerializer.Deserialize<MapDataModel>(jsonReader);
                        }

                        result = mapDataList;
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                    }
                }
            }

            return result;
        }

        public void Save(MapDataModel mapData, string fileInfo)
        {
            string json = JsonConvert.SerializeObject(mapData);
            File.WriteAllText(fileInfo, json);
        }
    }
}
