import requests
from os import getenv
from requests.structures import CaseInsensitiveDict

# The variables below will be passed to python through the Input.json file
url = 'https://api.geoapify.com/v2/places?categories=' + category +  '&filter=' + location  + '&limit=' + limit + '&apiKey=' + getenv('PLACES_TOKEN')

headers = CaseInsensitiveDict()
headers["Accept"] = "application/json"

# The data received from the API is passed to C# through the Output.json file
resp = requests.get(url=url, headers=headers)