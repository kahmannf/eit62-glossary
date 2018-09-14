require('dotenv').load();

const express = require('express');
const bodyparser = require('body-parser');

const config = require('./config');

const logger = require('./logger');

const routes = require('./routes');

const app = express();

app.use(bodyparser.json());

app.use('api', routes);

app.listen(config.server.port, () => {
  logger.log(`Server listening on port ${config.server.port}`, 10001)
});

