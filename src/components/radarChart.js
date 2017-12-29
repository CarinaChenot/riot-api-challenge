import * as d3 from 'd3';

//Taken from http://bl.ocks.org/mbostock/7555321
//Wraps SVG text
function wrapText(text, width) {
    text.each(function() {
        var text = d3.select(this),
            words = text.text().split(/\s+/).reverse(),
            word,
            line = [],
            lineNumber = 0,
            lineHeight = 1.4, // ems
            y = text.attr("y"),
            x = text.attr("x"),
            dy = parseFloat(text.attr("dy")),
            tspan = text.text(null).append("tspan").attr("x", x).attr("y", y).attr("dy", dy + "em");

        while (word = words.pop()) {
            line.push(word);
            tspan.text(line.join(" "));
            if (tspan.node().getComputedTextLength() > width) {
                line.pop();
                tspan.text(line.join(" "));
                line = [word];
                tspan = text.append("tspan").attr("x", x).attr("y", y).attr("dy", ++lineNumber * lineHeight + dy + "em").text(word);
            }
        }
    });
}

//Merge two objects
function merge(conf1, conf2) {
    if (typeof conf2 !== 'undefined') {
        for (var i in conf2) {
            if ('undefined' !== typeof conf2[i]) {
                conf1[i] = conf2[i];
            }
        }
    }
    return conf1;
}

//adapted from https://www.visualcinnamon.com/2015/10/different-look-d3-radar-chart.html
export default class RadarChart {

    constructor(id, axes, options) {

        var cfg = merge({
            w: 600, //Width of the circle
            h: 600, //Height of the circle
            margin: {
                top: 50,
                right: 100,
                bottom: 50,
                left: 100
            }, //The margins of the SVG
            levels: 5, //How many levels or inner circles should there be drawn
            labelFactorX: 1.35, //How much farther than the radius of the outer circle should the labels be placed
            labelFactorY: 1.15,
            wrapWidth: 160, //The number of pixels after which a label needs to be given a new line
            opacityArea: 0.35, //The opacity of the area of the blob
            dotRadius: 4, //The size of the colored circles of each blog
            opacityCircles: 0.1, //The opacity of the circles of each blob
            strokeWidth: 2, //The width of the stroke around each blob
            color: d3.scaleSequential(d3.interpolateViridis) //Color function
        }, options);

        //Variables
        var total = axes.length, //The number of different axes
            radius = Math.min(cfg.w / 2.0, cfg.h / 2.0), //Radius of the outermost circle
            angleSlice = Math.PI * 2 / total; //The width in radians of each "slice"

        //Scale for the radius
        var rScale = d3.scaleLinear().range([0, radius]).domain([0, 1]);

        //Remove svg if a previous chart was present
        d3.select(id).select("svg").remove();

        //Initiate the radar chart SVG
        var svg = d3.select(id).append("svg")
            .attr("width", cfg.w + cfg.margin.left + cfg.margin.right)
            .attr("height", cfg.h + cfg.margin.top + cfg.margin.bottom);

        //Append a g element 
        var g = svg.append("g")
            .attr("transform", "translate(" + (cfg.w / 2 + cfg.margin.left) + "," + (cfg.h / 2 + cfg.margin.top) + ")");

        //Append a glow filter
        var filter = g.append('defs').append('filter').attr('id', 'glow'),
            feGaussianBlur = filter.append('feGaussianBlur').attr('stdDeviation', '2.5').attr('result', 'coloredBlur'),
            feMerge = filter.append('feMerge'),
            feMergeNode_1 = feMerge.append('feMergeNode').attr('in', 'coloredBlur'),
            feMergeNode_2 = feMerge.append('feMergeNode').attr('in', 'SourceGraphic');

        //Wrapper for the grid & axes
        var axisGrid = g.append("g").attr("class", "axisWrapper");

        //Draw the background circles
        axisGrid.selectAll(".levels")
            .data(d3.range(1, cfg.levels + 1).reverse())
            .enter()
            .append("circle")
            .attr("class", "gridCircle")
            .style("fill", "#CDCDCD")
            .style("stroke", "#CDCDCD")
            .style("stroke-width", "1px")
            .style("stroke-dasharray", "3px")
            .style("fill-opacity", "0.1")
            .style("filter", "url(#glow)")
            .attr("r", function(d) {
                return radius / cfg.levels * d;
            });

        //Create the straight lines radiating outward from the center
        var axis = axisGrid.selectAll(".axis")
            .data(axes)
            .enter()
            .append("g")
            .attr("class", "axis");

        //Set up the small tooltip for when you hover over a circle	
        var tooltip = d3.select(id).append("div")
            .attr("class", "tooltip")
            .style("opacity", 0);

        //Append the lines
        axis.append("line")
            .attr("class", "line")
            .style("stroke", "gray")
            .style("stroke-opacity", "0.75")
            .style("stroke-width", "2px")
            .attr("x2", function(d, i) {
                return rScale(1.1) * Math.cos(angleSlice * i - Math.PI / 2);
            })
            .attr("y2", function(d, i) {
                return rScale(1.1) * Math.sin(angleSlice * i - Math.PI / 2);
            });

        //Append the labels at each axis
        axis.append("text")
            .attr("class", "legend")
            .style("font-size", "11px")
            .style("pointer-events", "all")
            .attr("text-anchor", "middle")
			.attr("cursor", "pointer")
            .attr("dy", "0.35em")
            .attr("x", function(d, i) {
                return rScale(cfg.labelFactorX) * Math.cos(angleSlice * i - Math.PI / 2);
            })
            .attr("y", function(d, i) {
                return rScale(cfg.labelFactorY) * Math.sin(angleSlice * i - Math.PI / 2);
            })
            .text(function(d) {
                return d.name;
            })
            .on("mouseover", function(d, i) {
                tooltip
                    .html(d.legend)
                    .style("left", (d3.event.pageX) + "px")
                    .style("top", (d3.event.pageY - 40) + "px")
                    .transition().duration(350).style("opacity", .9);
            })
            .on("mouseout", function(d) {
                tooltip.transition().duration(350).style("opacity", 0);
            })
            .call(wrapText, cfg.wrapWidth);

		this.id = id;
        this.g = g;
        this.rScale = rScale;
        this.angleSlice = angleSlice;
        this.cfg = cfg;
        this.tooltip = tooltip;
    }	

    setData(data) {

        var cfg = this.cfg,
            g = this.g,
            rScale = this.rScale,
            angleSlice = this.angleSlice,
            tooltip = this.tooltip;

        var radarLine = d3.radialLine()
            .curve(d3.curveCardinalClosed) //curveLinearClosed
            .radius(function(d) {
                return rScale(d.value);
            })
            .angle(function(d, i) {
                return i * angleSlice;
            });

        if (this.data != null) {
			
            if (this.data.length !== data.length) {
                g.selectAll(".radarWrapper").filter(function(d, i) {
                        return i >= data.length
                    }).attr("opacity", "1")
                    .transition().duration(1600).attr("opacity", "0").remove();
            }

            this.blobWrapper.data(data);
            this.blobBackground.data(data).transition().duration(1800).attr("d", radarLine);
            this.blobOutlines.data(data).transition().duration(1800).attr("d", radarLine);
            this.blobWrapper.selectAll(".radarCircle").data(function(d) {
                    return d;
                }).transition().duration(1800).attr("cx", function(d, i) {
                    return rScale(d.value) * Math.cos(angleSlice * i - Math.PI / 2);
                })
                .attr("cy", function(d, i) {
                    return rScale(d.value) * Math.sin(angleSlice * i - Math.PI / 2);
                });

        } else {

            //Create a wrapper for the blobs 
            var blobWrapper = this.blobWrapper = g.selectAll(".radarWrapper")
                .data(data)
                .enter().append("g")
                .attr("class", "radarWrapper");
   
            //Append the backgrounds 
            var blobBackground = this.blobBackground = blobWrapper.append("path")
                .attr("class", "radarArea")
                .attr("d", radarLine)
                .style("fill-opacity", 0.7)
                .style("fill", function(d, i) {
                    return cfg.color(i);
                })
                .on('mouseover', function(d, i) {
                    //Dim all blobs
                    /* const current = this;
                    d3.selectAll(".radarArea").filter(function(d, i) {
                    return (this !== current);
                    }).transition().duration(200)
                    .style("fill-opacity", 0.1);*/
                })
                .on('mouseout', function() {
                    //Bring back all blobs
                    /*  d3.selectAll(".radarArea")
                          .transition().duration(200)
                          .style("fill-opacity", 0.7);*/
                });

            //Create the outlines 
            var blobOutlines = this.blobOutlines = blobWrapper.append("path")
                .attr("class", "radarStroke")
                .attr("d", radarLine)
                .style("stroke-width", cfg.strokeWidth + "px")
                .style("stroke", function(d, i) {
                    return cfg.color(i);
                })
                .style("fill", "none")
                .style("filter", "url(#glow)").attr("stroke-width", "0px")

            //Append the dots
            blobWrapper.selectAll(".radarCircle")
                .data(function(d) {
                    return d;
                })
                .enter().append("circle")
                .attr("class", "radarCircle")
                .attr("r", cfg.dotRadius)
                .style("fill-opacity", 0.8)
                .attr("cx", function(d, i) {
                    return rScale(d.value) * Math.cos(angleSlice * i - Math.PI / 2);
                })
                .attr("cy", function(d, i) {
                    return rScale(d.value) * Math.sin(angleSlice * i - Math.PI / 2);
                })
                .style("fill", function(d, i, j) {
                    return cfg.color(j);
                });
        }

        this.data = data;
  
        //Wrapper for the invisible circles on top
        g.selectAll(".radarCircleWrapper").remove();
        var blobCircleWrapper = g.selectAll(".radarCircleWrapper")
            .data(data)
            .enter().append("g")
            .attr("class", "radarCircleWrapper");

        //Append a set of invisible circles on top for the mouseover pop-up
        blobCircleWrapper.selectAll(".radarInvisibleCircle")
            .data(function(d) {
                return d;
            })
            .enter().append("circle")
            .attr("class", "radarInvisibleCircle")
            .attr("r", cfg.dotRadius * 1.5)
            .attr("fill", "none")
			.attr("cursor", "pointer")
            .attr("cx", function(d, i) {
                return rScale(d.value) * Math.cos(angleSlice * i - Math.PI / 2);
            })
            .attr("cy", function(d, i) {
                return rScale(d.value) * Math.sin(angleSlice * i - Math.PI / 2);
            })
            .style("pointer-events", "all")
            .on("mouseover", function(d, i) {
				
                tooltip
                    .html(d.legend)
                    .style("left", (d3.event.pageX) + "px")
                    .style("top", (d3.event.pageY - 40) + "px")
                    .transition().duration(350).style("opacity", .9);
            })
            .on("mouseout", function(d, i) {

                tooltip.transition().duration(350)
                    .style("opacity", 0);
            });
    }
}