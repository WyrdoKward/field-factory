namespace FieldFactory.DataAccess.DTO
{
    public class FurnitureDTO
    {
        public string Id { get; set; }
        public string Json { get; set; }

        public FurnitureDTO(string idFurniture, string json)
        {
            Id = idFurniture;
            Json = json;
        }
    }
}
