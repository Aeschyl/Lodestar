// Packages
const express = require('express')
const bodyParser = require('body-parser');
const cors = require('cors');
const request = require('request');


// Package setup
const app = express();
app.use(cors());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());

// Firebase setup




// Endpoints
app.get('/onstart', (req, res) => {
});

app.get('/geoapify', (req, res) => {
  var geoUrl;
  geoUrl = req.query['url'] + `&apiKey=${process.env['GEOAPIFY_KEY']}`;
  request(geoUrl, { json: true }, (err, result, body) => {
    try {
      res.json(body);
    } catch {
      res.send("Error");
    }
  });
  geoUrl = null;
});

app.get('/ipinfo', (req, res) => {
  var ipUrl;
  ipUrl = req.query['url'] + `?token=${process.env['IPINFO_KEY']}`;
  request(ipUrl, { json: true }, (err, result, body) => {
    try {
      res.json(body);
    } catch {
      res.send("Error");
    }
  });
  ipUrl = null;
});

app.get('/bingmaps', (req, res) => {
  var bingMapsUrl;
  bingMapsUrl = req.query['url'] + `&key=${process.env['BINGMAPS_KEY']}`;
	request(bingMapsUrl, { json: true }, (err, result, body) => {
		try {
      // Gets only the coordinates and sends
		  res.json(body['resourceSets'][0]['resources'][0]['point']['coordinates']);
		}
		catch {
			res.send("Error");
		}
  });
  bingMapsUrl = null;
});

// Start server
app.listen();
