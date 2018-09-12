const path = require('path');
const webpack = require('webpack');
const bundleOutputDir = './wwwroot/';

module.exports = {
    mode: 'development',
    devtool: 'false',
    entry: {main: './Client/index.js'},
    output: {
        path: path.resolve(__dirname, bundleOutputDir),
        publicPath: '/dist/js/',
        filename: 'index_bundle.js'
    },
    module: {
        rules: [
            {test: /\.(js|jsx)$/, use: 'babel-loader', exclude: /node_modules/},
            {test: /\.css$/, use: ['style-loader', 'css-loader']}
        ]
    },
    plugins: [
        new webpack.SourceMapDevToolPlugin({
            filename: '[file].map', // Remove this line if you prefer inline source maps
            moduleFilenameTemplate: path.relative(bundleOutputDir, '[resourcePath]') // Point sourcemap entries to the original file locations on disk
        })
    ]
};