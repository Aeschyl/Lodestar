# Server Documentation

## Recommendations Endpoint
- https://TouristServer.sami200.repl.co/recommendations?cpuserialid=1234567890&long=10&lat=11
- In this endpoint, specify the CPU Serial ID for database. And specify the current latitude and longitude, preferably recieved from the built-in location sensor
- This will return to you a geoapify response with 5 locations that the user may like
## Geoapify Endpoint
- https://TouristServer.sami200.repl.co/geoapify?https%3A%2F%2Fapi.geoapify.com%2Fv2%2Fplaces%2F%3Fcategories%3Dcatering.fast_food%26filter%3Dcircle%3A11%2C12%2C80450%26bias%3Dproximity%3A11%2C12%26limit%3D10&cpuserialid=123456
- In this endpoint, specify the CPU Serial ID for database. And give a geoapify places api request without the API key, which is appended by the server
- This will return to you a geoapify response
## Parking Endpoint
- https://TouristServer.sami200.repl.co/parking?long=11&lat=12
- In this endpoint, give the latitude and longitude of the user
- This will return to you a geoapify response with 1 nearby parking location
## Weather Endpoint
- https://TouristServer.sami200.repl.co/weather?long=11&lat=12
- In this endpoint, give the latitude and longitude of the user
- This will return to you a openweathermap response with weather information in imperial units(Farenheit), it also has a key for an image which will need to be displayed using url
## IPInfo
- May be deprecated
- Provide the prebuilt url as a "url" parameter and this will return the full body respose
## BingMaps
- Provide the full url as a "url" parameter
- Simply the coordinates will be returned to you
