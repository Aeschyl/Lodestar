# Lodestar
## An app to help users find tourist attractions in Colorado

![Logo](https://user-images.githubusercontent.com/72280649/151738750-266236f3-d125-4c55-a18b-3a5e23e5a29f.png)

### Download

[Download source code and build app yourself](https://github.com/Aeschyl/FBLA-Attractions-App/archive/refs/heads/master.zip)

[Download latest app build](https://github.com/Aeschyl/Lodestar/releases/download/v0.0.3/Lodestar.zip)


## Lodestar Client Documentation

### How to use

The user is welcomed by a splash screen and led through easy to understand and interactive screens in the app that let them apply the filters they require to search for attractions nearby. Eventually they reach the final screen which displays the results on a map. The user can click on a marker on the map to know more about the place.

For the best user experience, the app provides 3 different options to procure the user's location:

1. Using the built-in location sensor of the computer to obtain the coordinates (recommended)
2. The option for the user to manually enter their address
3. Using the Public IP Address of the user to determine their location (note completely accurate but maintains better privacy)

Moreover, in the distance section, the app provides two options of deciding how far the user is willing to go:

1. The first one is a simple slider that lets the user decide how far they are willing to go (ranging from 1 mile to 20 miles)

### Tools used

This application implements WPF Framework in C# language for frontend. It uses NodeJS for the backend server, this is because sensitive api keys are attatched in the server and the relevant data is returned to the C#, hence securing the api keys from any possible compromises

The server uses the express and request packages from npm

The app implements different APIs to enable different methods of obtaining the location of the user, these apis are managed by the server.

For the option of manually entering their address, the server makes a call to the Bing Geocoding API to find the coordinates and returns it to C#

For the IP Address option, the server procures the user's IP Address and then calls the [IPinfo API](https://ipinfo.io/products/ip-geolocation-api) to find the coordinates

Finally the server makes calls to Places API provided by Geoapify to return the required results of various different attractions. [Places API](https://www.geoapify.com/places-api)

The application uses Bing Maps to generate the images

## Lodestar Server Documentation

Visit [Lodestar Server](https://replit.com/@Sami200/TouristServer)

### Recommendations Endpoint
- https://TouristServer.sami200.repl.co/recommendations?cpuserialid=1234567890&long=10&lat=11
- In this endpoint, specify the CPU Serial ID for database. And specify the current latitude and longitude, preferably recieved from the built-in location sensor
- This will return to you a geoapify response with 5 locations that the user may like
### Geoapify Endpoint
- https://TouristServer.sami200.repl.co/geoapify?url=https%3A%2F%2Fapi.geoapify.com%2Fv2%2Fplaces%2F%3Fcategories%3Dcatering.fast_food%26filter%3Dcircle%3A11%2C12%2C80450%26bias%3Dproximity%3A11%2C12%26limit%3D10&cpuserialid=123456
- In this endpoint, specify the CPU Serial ID for database. And give a geoapify places api request without the API key, which is appended by the server
- This will return to you a geoapify response
### Parking Endpoint
- https://TouristServer.sami200.repl.co/parking?long=11&lat=12
- In this endpoint, give the latitude and longitude of the user
- This will return to you a geoapify response with 1 nearby parking location
### Weather Endpoint
- https://TouristServer.sami200.repl.co/weather?long=11&lat=12
- In this endpoint, give the latitude and longitude of the user
- This will return to you a openweathermap response with weather information in imperial units(Farenheit), it also has a key for an image which will need to be displayed using url
### IPInfo
- May be deprecated
- Provide the prebuilt url as a "url" parameter and this will return the full body respose
### BingMaps
- Provide the full url as a "url" parameter
- Simply the coordinates will be returned to you

## How to Use
### Main Screen
- Shown when you open the application
- Has recommendations of locations that you may be interested in
- Press the blue `Find Attractions!` button to start searching
  - If you mess up in any of the searching process, press the blue `Restart` button at the bottom left
### Category Page
- Select one of the options
- This step is required
### Sub-Category Page
- Select any of these to narrow down your search
- This step is optional
### Amenities Page
- You may select multiple options for this page
- These allow you to narrow down locations to fit any accessibility needs
- This is optional
### Location Page
- Select any of the options, typing in your address is the most accurate
- This step is required
### Distance Page
- Move the slider to the amount of miles within which you would like your location
- If you skip this, it will only search within a 1 mile radius, which may not show the most locations
### Results Page
- This page has a map
  - You can click an orange marker on this map to get details on it
- This page also has a list of locations
  - You can also click on these to get details on it
- The details include of the attraction, distance, and weather conditions
### Extra Pages
- These pages don't pertain to finding attractions
- Press the info button on the top left to locate these pages
#### FAQ Page
- This page answers some commonly asked questions about the app
- Click the circle with a triangle inside it to locate the answer
#### Copyright Page
- This page contains resources used to create the app
- Press the blue contact us button in the bottom right to send us an email

