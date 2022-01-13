// Packages
const express = require('express')
const bodyParser = require('body-parser');
const cors = require('cors');
const request = require('request');
const firebase = require('firebase');
// Package setup
const app = express();
app.use(cors());
app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());

const firebaseConfig = {
  apiKey: process.env['FIREBASE_KEY'],
  authDomain: process.env['authDomain'],
  databaseURL: process.env['databaseURL'],
  projectId: process.env['projectId'],
  storageBucket: process.env['storageBucket'],
  messagingSenderId: process.env['messagingSenderId'],
  appId: process.env['appId'],
  measurementId: process.env['measurementId']
};

// Firebase setup
const firebaseConfig = {
  apiKey: process.env['FIREBASE_API_KEY'],
  authDomain: process.env['FIREBASE_AUTH_DOMAIN'],
  projectId: process.env['FIREBASE_PROJECT_ID'],
  storageBucket: process.env['FIREBASE_STORAGE_BUCKET'],
  messagingSenderId: process.env['FIREBASE_MESSAGING_SENDER_ID'],
  appId: process.env['FIREBASE_APP_ID']
};

firebase.initializeApp(firebaseConfig);

let database = firebase.database();

/*
writing
database.ref("customPath").set(obj, function(error) {
    if (error) {
      // The write failed...
      console.log("Failed with error: " + error)
    } else {
      // The write was successful...
      console.log("success")
    }
})

reading
database.ref('customPath').once('value')
.then(function(snapshot) {
    console.log( snapshot.val() )
})
*/

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
