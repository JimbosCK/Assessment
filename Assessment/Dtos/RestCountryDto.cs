namespace Assessment.Dtos {

    public class CountryNameDto {
        public string Common { get; set; }
    }

    public class RestCountryDto {
        public CountryNameDto Name { get; set; }

        public string[] Capital { get; set; }

        public string[] Borders { get; set; }
    }
}
