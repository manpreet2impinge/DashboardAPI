using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace DashboardAPI.Common.Helpers
{
    public static class JsonUtility<T>
    {
        /// <summary>
        /// Method to load from json files.
        /// </summary>
        /// <param name="resourcePath"></param>
        /// <exception cref="FileNotFoundException">If the file is not found at the provided path.</exception>>
        /// <returns></returns>
        public static IEnumerable<T> LoadFromJson(string resourcePath, Assembly assembly)
        {
            //var x = assembly.GetManifestResourceNames();
            //var outPutDirectory = Path.GetDirectoryName(assembly.CodeBase);
            //var outPutDirectory1 = Path.GetDirectoryName(Assembly.GetCallingAssembly().CodeBase);

            var path = assembly.GetManifestResourceStream(resourcePath);
            if (path == null)
            {
                throw new FileNotFoundException();
            }

            using (var streamReader = new StreamReader(path))
            {
                var json = streamReader.ReadToEnd();
                return JsonSerializer.Deserialize<List<T>>(json);
            }
        }

        // public static T LoadFromJson(string filename)
        // {
        //     T resultObj;
        //     var path = HostingEnvironment.MapPath(@"/" + filename);

        //     if (File.Exists(path))
        //     {
        //         using (StreamReader sr = new StreamReader(path))
        //         {
        //             resultObj = JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
        //         }
        //     }
        //     else
        //     {
        //         throw new FileNotFoundException();
        //     }
        //     return resultObj;            
        // }
    }
}