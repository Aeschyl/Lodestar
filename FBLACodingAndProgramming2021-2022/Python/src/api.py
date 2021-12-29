import requests
from requests.structures import CaseInsensitiveDict
import json
import os
from pathlib import Path

dir_path = os.path.dirname(os.path.realpath(__file__))
path1 = Path(dir_path)
parentPath = str(path1.parent)

# Fetching data from Input.json
with open(parentPath + "\\src\\Input\\Input.json", "r") as infile:
    user_inputs = json.loads(infile.read())

url = 'https://api.geoapify.com/v2/places'
headers = CaseInsensitiveDict()
headers["Accept"] = "application/json"

# Checking if there is a subcategory defined
if user_inputs['subcategory'] == "":
    categories = user_inputs['category']
else:
    categories = user_inputs['category'] + '.' + user_inputs['subcategory']

# Translating the amenities list to a readable form for the API
conditions = ""
if len(user_inputs['amenities']) == 0:
    # Parameters for the Places API
    params = dict(
        categories = categories,
        filter = 'rect:11.549881365729718,48.15114774722076,11.58831616443861,48.12837326392079', # Dummy Parameter
        limit = '20',
        apiKey = '233315ef9be94184b7addc54c006bbde'
    )
else:
    for i in user_inputs['amenities']:
        conditions += i + ','
    conditions = conditions[:-1]

    # Parameters for the Places API
    params = dict(
        categories = categories,
        conditions = conditions,
        filter = 'rect:11.549881365729718,48.15114774722076,11.58831616443861,48.12837326392079', # Dummy Parameter
        limit = '20',
        apiKey = '233315ef9be94184b7addc54c006bbde'
    )

# Calling the Places API and storing received data in a variable
response = requests.get(url=url, params=params, headers=headers).json()

# Serializing json 
json_object = json.dumps(response, indent = 4)
  
# The data received from the API is passed to C# through the output.json file
with open(parentPath + "/src//Output/Output.json", "w") as outfile:
    outfile.write(json_object)