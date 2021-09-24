const fs = require('fs')

const port = 3000
const host = '::'

require('http').createServer().on('error', (err) => console.error(err)).on('listening', () => {
  let path = `http://`
  if (host.includes(':'))
    path += `[${host}]`
  else
    path += host
  path += `:${port}`
  console.log(`server listening on ${path}`)
}).on('request', (req, res) => {
  let path = req.url.split(/\?|#/)[0]
  if (path === '/favicon.ico') {
    res.statusCode = 404
    res.end()
  } else {
    if (path === '/')
      path += 'index'
    path = `${__dirname}${path}.json`
    fs.readFile(path, (err, data) => {
      if (err) {
        console.warn(`Cannot load file from ${path}`)
        res.statusCode = 404
        res.end(err.message)
      } else {
        console.log(`load file from ${path}`)
        res.setHeader('Content-Type', 'application/json')
        res.end(data)
      }
    })
  }
}).listen(port, host)