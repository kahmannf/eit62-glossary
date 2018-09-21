const config = require('../config'); 

const internal_logger = (() => {
  switch(config.server.logger) {
    case 'console':
      return require('./console-logger')
  }
})();



const log = (message, level = 0) => {
  if(level >= config.server.loglevel) {
    internal_logger.log(message);
  }
}

const error = (error) => {
  internal_logger.error(error);
}


const dir = (obj, level = 0) => {
  if(level >= config.server.loglevel) {
    internal_logger.dir(obj);
  }
}


module.exports = {
  log,
  error,
  dir
}