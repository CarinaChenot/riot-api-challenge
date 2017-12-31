# Runes playstyle
 
## Description

How many time I've heard this sentence **"What is the rune page for playing /insert champion name/ please ?!"** More than a  thousand.
We want to show with our project that there's no better rune page except the one that fit your playstyle and your objectives.

We wanted to build it around a radar chart that would be to depict the influence of a particular runes set on the game, to highlight what a particular rune can bring to you and how that can be use to win the match or bring something special to your team.

![Radar chart](https://img15.hostingpics.net/pics/784521axis.png)

The UI is quite basic, you choose a champion and a lane. Then we bring some analytics data to the user.

There's a couple of differents sets and no one better than another so we bring on the right the most played sets for the champion on the lane and the sets who have a better winrate. 
The user can click on them to see more detailled data on the chart

![Radar chart](https://img15.hostingpics.net/pics/962869axis2.png)

On the left the user can choose custom runes and to see that would be the impact on them on the game (if of course we have matching data !).

## Data Generation

Since all the runes stats come from match data we had to crawl a lot of match to build efficient stats.
So we built a match crawler that insert them into a Mongo Database.
There we query Mongo to get .json files of analytics data that the front website can use seamlessly.



## Build Setup

``` bash
# install dependencies
npm install

# serve with hot reload at localhost:8080
npm run dev

# build for production with minification
npm run build

# build for production and view the bundle analyzer report
npm run build --report
```

For a detailed explanation on how things work, check out the [guide](http://vuejs-templates.github.io/webpack/) and [docs for vue-loader](http://vuejs.github.io/vue-loader).
