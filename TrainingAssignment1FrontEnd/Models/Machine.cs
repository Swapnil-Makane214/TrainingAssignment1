namespace TrainingAssignment1FrontEnd.Models
{
    public class Machine
    {
        public string? Id { get; set; }
        public string? machineName { get; set; }
        public List<Asset>? assetsUsed { get; set; } = new();


        public void AddAsset(Asset Current)
        {
            assetsUsed?.Add(Current);
        }
    }
}
