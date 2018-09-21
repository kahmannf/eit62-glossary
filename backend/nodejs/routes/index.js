const express = require('express');
const router = express.Router();

const secure = require('./secure');
const public = require('./public');

router.use('secure', secure);
router.use(public);

module.exports = router;
