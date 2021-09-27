const fs = require('fs')

const exists = (path) => new Promise((resolve, reject) => {

  console.log(path)
  fs.stat(path, (err, stats) => {
    if (err)
      resolve(false)
    else {
      console.log(stats)
      resolve(stats.isFile())
    }
  })
})
const readFile = (path) => new Promise((resolve, reject) => {
  fs.readFile(path, 'utf8', (err, data) => {
    if (err)
      reject(err)
    else
      resolve(data)
  })
})

module.exports = {
  exists,
  readFile
}