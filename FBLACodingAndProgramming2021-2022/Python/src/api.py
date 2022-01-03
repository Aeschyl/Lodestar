import requests
import json
import os
from pathlib import Path
import urllib.request

# Gets the secret API Key for a particular service (temporary solution)
def get_api_key(api_name):
    secret_keys = {'PLACES_API': '233315ef9be94184b7addc54c006bbde', 'IP_TO_LOC_API': 'afafcbd31ed7de'}
    return secret_keys[api_name]


# Checking for internet connections
def check_connection(output_path):
    try:
        urllib.request.urlopen('http://google.com') # Trusted domain to check whether the user is connected to internet
        return True
    except:
        return False


def defining_parameters(user_inputs):
    # Checking if there is a subcategory defined
    if user_inputs['selections']['subcategory'] == "":
        categories = user_inputs['selections']['category']
    else:
        categories = user_inputs['selections']['category'] + '.' + user_inputs['selections']['subcategory']

    location = managing_location(user_inputs['location']['option'], user_inputs['location']['data'])

    location_filter = managing_distance(location, user_inputs['distance']['option'], user_inputs['distance']['data'])

    limit = '20'

    params = dict(
        categories = categories,
        filter =  location_filter,
        bias = 'proximity:' + location,
        limit = limit,
        apiKey = get_api_key('PLACES_API')
    )

    # Translating the amenities list to a readable form for the API
    if len(user_inputs['selections']['amenities']) != 0:
        conditions = ""
        for i in user_inputs['selections']['amenities']:
            conditions += i + ','
        conditions = conditions[:-1]

        # Adding amenities to the parameters
        params['conditions'] = conditions

    return params


# Gets the location of the user
def managing_location(option, data):
    if int(option) == 1:  # Using in-built location sensor, already handled by c#
        location = data
    elif int(option) == 2: # Will build later if decide to port from c# to python handling the manual address geocoding
        location = data
    elif int(option) == 3: # Using the IP Address of the user to determine location (slightly inaccurate)
        ip = requests.get(url='https://api.ipify.org', timeout=5).content.decode('utf8')
        location = requests.get(url=f"https://ipinfo.io/{ip}/json", headers={"Accept": "application/json"}, params={"token": get_api_key('IP_TO_LOC_API')}, timeout=5).json()
        location = str(location['loc'])

    location = location.split(',')[1] + ',' +location.split(',')[0]
    return location


# Defines the region of within to find the desired attractions
def managing_distance(location, option, data):
    if int(option) == 1:
        radius = int(int(data) * 1609.34) # Converting from miles to meters
        location_filter = 'circle:' + location + ',' + str(radius)
    return location_filter


if __name__ == '__main__':   
    dir_path = os.path.dirname(os.path.realpath(__file__))
    path1 = Path(dir_path)
    parent_path = str(path1.parent)

    input_path = parent_path + '\\src\\Input\\Input.json'
    output_path = parent_path + '\\src\\Output\\Output.json'

    if not check_connection(output_path):
        with open(output_path, "w") as outfile:
            outfile.write(json.dumps({"Error":"Connection Error - Please check your internet connection"}))
        quit()

    # Fetching data from Input.json
    with open(input_path, "r") as infile:
        user_inputs = json.loads(infile.read())
    
    params = defining_parameters(user_inputs)
    
    # Calling the Places API
    response = requests.get(url='https://api.geoapify.com/v2/places', headers={"Accept": "application/json"}, params=params, timeout=5).json()
    result_json = json.dumps(response, indent = 4)

    # The data received from the API is passed to C# through the output.json file
    with open(output_path, "w") as outfile:
        outfile.write(result_json)