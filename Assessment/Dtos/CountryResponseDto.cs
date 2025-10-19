namespace Assessment.Dtos {
    public class CountryResponseDto {
        public string Name { get; set; }

        public string Capital { get; set; }

        public IEnumerable<string> Borders { get; set; }
    }
}
