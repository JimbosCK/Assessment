# Assessment Project

This repository contains an ASP.NET Core Web API project that implements solutions to the following requirements:

## Project Description

The Web API provides two main functionalities:

1. **Find the Second Largest Integer**
   - A POST endpoint that receives a JSON body containing an array of integers and returns the second largest integer in the array.

2. **Retrieve and Cache Country Data**
   - A GET endpoint that retrieves country data from the [Rest Countries API](https://restcountries.com/), saves the data to a Microsoft SQL Server database and a local memory cache, and returns the data. The endpoint prioritizes fetching data from the cache, then the database, and finally the external API if the data is not available locally.

---

## API Endpoints

### 1. Find the Second Largest Integer

**Endpoint:**
```
POST api/Math/SecondLargest
```

**Request Body:**
```json
{
  "requestArrayObj": [1, 3, 5, 7, 9]
}
```

**Response:**
```json
7
```

**Error Responses:**
- **400 Bad Request:** 
- If the input array is null or empty.
  ```json
  {
    "error": "The collection cannot be null or empty."
  }
  ```
- If the input array contains fewer than two distinct integers.
  ```json
  {
    "error": "The collection must contain at least two distinct numbers."
  }
  ```
- If the input array contains elements that are not integers.
  ```json
  {
    "error": "The array contains elements that are not valid integers."
  }
  ```


**Description:**
This endpoint accepts a JSON object containing an array of integers (`requestArrayObj`) and returns the second largest integer in the array.

---

### 2. Retrieve and Cache Country Data

**Endpoint:**
```
GET /api/Countries/All
```

**Response:**
```json
[
    {
        "name": "Lithuania",
        "capital": "Vilnius",
        "borders": [
            "BLR",
            "LVA",
            "POL",
            "RUS"
        ]
    },
    {
        "name": "Chile",
        "capital": "Santiago",
        "borders": [
            "ARG",
            "BOL",
            "PER"
        ]
    },
    .
    .
    .
    }
]
```

**Error Responses:**
- **500 Internal Server Error:** If there is a backend/server issue.
  ```json
  {
    "error": "An unexpected error occurred while processing the request."
  }

- **503 Service Unavailable Error:** If there is an issue with the external API.
  ```json
  {
    "error": "The third-party country API is currently unavailable."
  }
  ```

**Description:**
This endpoint retrieves country data, including the common name, capital, and borders of each country. The data is fetched from the following sources in order of priority:
1. Local memory cache
2. Microsoft SQL Server database
3. [Rest Countries API](https://restcountries.com/)

If the data is fetched from the external API, it is saved to both the cache and the database for future requests.

---

## How to Run the Project

1. Clone the repository:
   ```bash
   git clone https://github.com/JimbosCK/Assessment.git
   ```

2. Open the solution file `Assessment.sln` in Visual Studio.

3. Configure the connection string for the Microsoft SQL Server.

4. Build the project and run migrations.

5. Run.

---

## Technologies Used
- ASP.NET Core Web API
- .NET 9.0
- Microsoft SQL Server
- MemoryCache
- Rest Countries API
