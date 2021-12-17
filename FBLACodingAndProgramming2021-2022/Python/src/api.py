import requests
from os import getenv
from requests.structures import CaseInsensitiveDict
import json

#defining dummy parameters, will get actual data from Input.json later
category = 'accommodation.hotel'
location = 'rect:11.549881365729718,48.15114774722076,11.58831616443861,48.12837326392079'
limit = '20'

# Defining the url to request the data from
url = 'https://api.geoapify.com/v2/places?categories=' + category +  '&filter=' + location  + '&limit=' + limit + '&apiKey=' + '233315ef9be94184b7addc54c006bbde' # <-- Places API key

headers = CaseInsensitiveDict()
headers["Accept"] = "application/json"

# The data received from the API
response = requests.get(url=url, headers=headers).json()

# Serializing json 
json_object = json.dumps(response, indent = 4)
  
# The data received from the API is passed to C# through the output.json file
with open("./Output/Output.json", "w") as outfile:
    outfile.write(json_object)