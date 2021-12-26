const express = require('express');
const fetch = require('cross-fetch');
const app = express()
const port = 3000
const CONFIG = {
    TOKEN: "233315ef9be94184b7addc54c006bbde"
};
app.get('/tourists', (req, res) => {
    let uInputs, categories, conditions, params, result;
    conditions = "";
    uInputs = req.params;
    if (uInputs['subcategory'].length > 0) {
        categories = uInputs['category'] + '.' + uInputs['subcategory'];
    } else {
        categories = uInputs['category'];
    }
    if (uInputs['amenities'].length == 0) {
        params = {
            "apiKey": config.TOKEN,
            "categories": categories,
            "filter": 'rect:11.549881365729718,48.15114774722076,11.58831616443861,48.12837326392079', // Dummy Parameter
            "limit": '20'
        };
    } else {
        conditions = uInputs['amenities'];
        params = {
            "apiKey": config.TOKEN,
            "categories": categories,
            "conditions": conditions,
            "filter": 'rect:11.549881365729718,48.15114774722076,11.58831616443861,48.12837326392079', // Dummy Parameter
            "limit": '20'
        }
    }
    fetch('https://api.geoapify.com/v2/places', { method: 'GET', headers: params})
            .then(res => res.json())
            .then(json => res.end(json));
                
});

app.listen(port, () => {
    console.log(`app listening at http://localhost:${port}`)
});
