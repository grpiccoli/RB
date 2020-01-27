var chart = am4core.create("newchartdiv", am4maps.MapChart);
chart.geodata = am4geodata_LLlow;
chart.projection = new am4maps.projections.Miller();
var polygonSeries = chart.series.push(new am4maps.MapPolygonSeries());
polygonSeries.useGeodata = true;
var polygonTemplate = polygonSeries.mapPolygons.template;
polygonSeries.heatRules.push({
    "property": "fill",
    "target": polygonTemplate,
    "min": am4core.color("gold"),
    "max": am4core.color("darkred")
});
polygonTemplate.tooltipText = "{name}: {value} Ton";
polygonTemplate.events.on("hit", function (ev) {
    chart.zoomToMapObject(ev.target);
});
var heatLegend = chart.createChild(am4maps.HeatLegend);
heatLegend.valign = "bottom";
heatLegend.align = "left";
heatLegend.series = polygonSeries;
heatLegend.width = am4core.percent(100);
heatLegend.orientation = "horizontal";
heatLegend.padding(20, 20, 20, 20);
heatLegend.valueAxis.renderer.labels.template.fontSize = 10;
heatLegend.valueAxis.renderer.minGridDistance = 50;
var dataSource = polygonSeries.dataSource;
dataSource.url = "/Arrivals/Data";
polygonTemplate.events.on("over", function (ev) {
    if (!isNaN(ev.target.dataItem.value)) {
        heatLegend.valueAxis.showTooltipAt(ev.target.dataItem.value);
    }
    else {
        heatLegend.valueAxis.hideTooltip();
    }
});
chart.exporting.menu = new am4core.ExportMenu();
polygonTemplate.events.on("out", function (_ev) {
    heatLegend.valueAxis.hideTooltip();
});
chart.zoomControl = new am4maps.ZoomControl();
chart.zoomControl.valign = "bottom";