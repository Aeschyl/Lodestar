# Tourist Attractions App

![Hey](./FBLACodingAndProgramming2021-2022/Assets/logo.png)

## Download

[Download source code and build app yourself](https://github.com/Aeschyl/FBLA-Attractions-App/archive/refs/heads/master.zip)

[Download latest app build](#) _(Add link for this eventually)_

## How to use

The user is welcomed by a splash screen and led through easy to understand and interactive screens in the app that let them apply the filters they require to search for attractions nearby. Eventually they reach the final screen which displays the results on a map. The user can click on a marker on the map to know more about the place.

For the best user experience, the app provides 3 different options to procure the user's location:

1. Using the built-in location sensor of the computer to obtain the coordinates (recommended)
2. The option for the user to manually enter their address
3. Using the Public IP Address of the user to determine their location (note completely accurate but maintains better privacy)

Moreover, in the distance section, the app provides two options of deciding how far the user is willing to go:

1. The first one is a simple slider that lets the user decide how far they are willing to go (ranging from 1 mile to 20 miles)
2. The second one gameifies the app and improves user experience by providing the option of drawing a box or circle around their location letting them decide how far they are willing to go (not yet implemented)

## Tools used

This application implements WPF Framework in C# language for frontend and the C# for the backend code as well

The app implements different APIs to enable different methods of obtaining the location of the user

For the option of manually entering their address, the app makes a call to the Bing Geocoding API to find the coordinates

For the IP Address option, the app procures the user's IP Address and then calls the [IPinfo API](https://ipinfo.io/products/ip-geolocation-api) to find the coordinates

Finally the app makes calls to Places API provided by Geoapify to return the required results of various different attractions. [Places API](https://www.geoapify.com/places-api)
