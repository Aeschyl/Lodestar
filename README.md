# Lodestar <img src="https://user-images.githubusercontent.com/72280649/151738750-266236f3-d125-4c55-a18b-3a5e23e5a29f.png" alt="Logo" width="40"/>

## An app to help users find tourist attractions in Colorado

### Download

[Download source code and build app yourself](https://github.com/Aeschyl/FBLA-Attractions-App/archive/refs/heads/master.zip)

[Download latest app build](https://github.com/Aeschyl/Lodestar/releases/download/v0.0.3/Lodestar.zip)

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
### Get Favorites
- https://touristserver.sami200.repl.co/getFavorites?cpuserialid=BFEBFBFF000806E9
- Returns object like:
  - Favorites
    - 0
      - address
      - imgLink
      - lat
      - lon
      - name
### Add Favorite
- https://touristserver.sami200.repl.co/addFavorite?cpuserialid=BFEBFBFF000806E9
- With post object that us URL encoded like
  - {
    "name": "Truffnies",
    
    "placeID": "5126e1421ec1385ac059460c516628ce4340f00103f901c8ede63b0100000092030954727566666e696573",
    
    "imgLink": "https%3A%2F%2Fencrypted-tbn0.gstatic.com%2Fimages%3Fq%3Dtbn%3AANd9GcT8KPVTqPtZY5zjdlU5ed6o8AjZH9B_GLYi-nQ37GQ1aLpD-CUGGnLQ6E5t%26s",
    
    "address":"8765%20East%20Orchard%20Road,%20Greenwood%20Village,%20CO%2080111,%20United%20States%20of%20America",
    
    "lon":-104.886787,
    
    "lat":39.610607
}
- Returns nothing, just put in the parameters

## Lodestar Client Documentation
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
### Advanced Features
#### Recommendations
- Recommendations are provided after the privacy policy page
- They are given in a format with an image, name, and address
- These recommendations are computed using your previous selections inside the app to determine what places you would be most interested in
- You do not need to have used the app to get recommendations
#### Favorites
- Next to an event on the results page you can press the small heart to favorite it
- You will be able to later find all the info on what you favorited
- All your favorites are stored in the tab favorites below the info section

