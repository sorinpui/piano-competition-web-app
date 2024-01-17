using System.Text.Json.Serialization;

namespace CompetitionApi.Application.Requests
{
    public class CreateRenditionRequest
    {
        [JsonRequired]
        public string Name { get; set; }

        [JsonRequired]
        public string Composer { get; set; }

        [JsonRequired]
        public string Period { get; set; }
    }
}
