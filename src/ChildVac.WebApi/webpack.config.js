"use strict";
const path = require('path');
const VueLoaderPlugin = require('vue-loader/lib/plugin')
const bundleOutputDir = './wwwroot/dist';

module.exports = {
    mode: 'development',
    stats: { modules: false },
    context: __dirname,
    
    entry: { 'main': './ClientApp/boot.js' },
    output: {
        path: path.resolve(__dirname, bundleOutputDir),
        filename: '[name].js',
        publicPath: 'dist/'
    },
    module: {
        rules: [
            {
                test: /\.vue$/,
                loader: 'vue-loader'
            },
            {
                test: /\.js$/,
                loader: 'babel-loader'
            },
            {
                test: /\.css$/,
                use: [
                    'vue-style-loader',
                    'css-loader'
                ]
            },
            {
                test: /\.(png|jpg|gif|ttf)$/i,
                use: [
                    {
                        loader: 'url-loader',
                        options: {
                            limit: 8192
                        }
                    }
                ]
            }
        ]
    },
    resolve: {
        extensions: [
            '.js',
            '.vue'
        ]
    },
    plugins: [
        new VueLoaderPlugin()
    ]
};