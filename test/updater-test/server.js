const fs = require('./file')

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
}).on('request', async (req, res) => {
  let path = req.url.split(/\?|#/)[0]

  if (path === '/')
    path += 'index'
  path = `${__dirname}${path}`

  try {
    if (await fs.exists(path)) {
      if(path.includes('favicon'))
        res.setHeader('Content-Type', 'image/png')
      else
        res.setHeader('Content-Type', 'text/plain')
    } else {
      path += '.json'
      res.setHeader('Content-Type', 'application/json')
    }
    const data = await fs.readFile(path)
    console.log(`load file from ${path}`)
    res.end(data)
  } catch (err) {
    console.warn(`Cannot load file from ${path}`)
    res.statusCode = 404
    res.end()
  }
}).listen(port, host)