module.exports = {
  pages: {
    'index': {
      entry: './src/pages/Home/main.js',
      template: 'public/index.html',
      title: 'Home',
      chunks: [ 'chunk-vendors', 'chunk-common', 'index' ]
    },
    'world': {
      entry: './src/pages/World/main.js',
      template: 'public/index.html',
      title: 'World Map',
      chunks: [ 'chunk-vendors', 'chunk-common', 'world' ]
    }
  },
  devServer: {
      //port: 5000,
      proxy: 'http://localhost:5000',
  }
  
}
