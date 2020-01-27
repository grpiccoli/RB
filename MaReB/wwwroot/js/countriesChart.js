am4core.useTheme(am4themes_animated);
var chart = am4core.create("chartdiv", am4maps.MapChart);
chart.geodata = am4geodata_worldLow;
chart.projection = new am4maps.projections.Miller();
var polygonSeries = chart.series.push(new am4maps.MapPolygonSeries());
polygonSeries.exclude = ["AQ"];
polygonSeries.useGeodata = true;
chart.zoomControl = new am4maps.ZoomControl();
chart.zoomControl.valign = "bottom";
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
polygonTemplate.events.on("over", function (ev) {
    if (!isNaN(ev.target.dataItem.value)) {
        heatLegend.valueAxis.showTooltipAt(ev.target.dataItem.value);
    }
    else {
        heatLegend.valueAxis.hideTooltip();
    }
});
polygonTemplate.events.on("out", function (_ev) {
    heatLegend.valueAxis.hideTooltip();
});
var dataSource = polygonSeries.dataSource;
dataSource.url = "/countries/data/0?p=true";

var lineSeries = chart.series.push(new am4maps.MapLineSeries());
var lineTemplate = lineSeries.mapLines.template;
lineSeries.mapLines.template.strokeWidth = 0.4;
lineTemplate.stroke = chart.colors.getIndex(1).brighten(-0.5);
lineSeries.dataSource.url = "/countries/lines/0";
lineSeries.mapLines.template.shortestDistance = false;

var imageSeries = chart.series.push(new am4maps.MapImageSeries());
var imageTemplate = imageSeries.mapImages.template;
imageTemplate.tooltipText = "{title}";
imageTemplate.nonScaling = true;
var marker = imageTemplate.createChild(am4core.Circle);
marker.fill = chart.colors.getIndex(1).brighten(-0.5);
imageTemplate.propertyFields.latitude = "latitude";
imageTemplate.propertyFields.longitude = "longitude";
imageSeries.dataSource.url = "/countries/images/0";
marker.radius = 3;
marker.strokeWidth = 1;

var pieSeries = chart.series.push(new am4maps.MapImageSeries());
var pieTemplate = pieSeries.mapImages.template;
pieTemplate.propertyFields.latitude = "latitude";
pieTemplate.propertyFields.longitude = "longitude";

var pieChartTemplate = pieTemplate.createChild(am4charts.PieChart);
pieChartTemplate.adapter.add("data", function (_data, target) {
    if (target.dataItem) {
        return target.dataItem.dataContext.pieData;
    }
    else {
        return [];
    }
});
pieChartTemplate.propertyFields.width = "width";
pieChartTemplate.propertyFields.height = "height";
pieChartTemplate.horizontalCenter = "middle";
pieChartTemplate.verticalCenter = "middle";

var pieTitle = pieChartTemplate.titles.create();
pieTitle.text = "{title}";

var pieSeriesTemplate = pieChartTemplate.series.push(new am4charts.PieSeries);
pieSeriesTemplate.dataFields.category = "category";
pieSeriesTemplate.dataFields.value = "value";
pieSeriesTemplate.labels.template.disabled = true;
pieSeriesTemplate.ticks.template.disabled = true;

pieSeries.dataSource.url = "/countries/continents?p=true";

chart.exporting.menu = new am4core.ExportMenu();

function changeData(value) {
    if (value.length < 2) {
        value = ["0", "True"];
        $(".selectpicker").val(value);
    }
    dataSource.url = "/countries/data/" + value[0] + "?p=" + value[1];
    dataSource.load();
    lineSeries.dataSource.url = "/countries/lines/" + value[0];
    lineSeries.dataSource.load();
    imageSeries.dataSource.url = "/countries/images/" + value[0];
    imageSeries.dataSource.load();
    pieSeries.dataSource.url = "/countries/continents?p=" + value[1];
    pieSeries.dataSource.load();
    var unit = value[1] === "True" ? "Ton" : "FOB";
    polygonTemplate.tooltipText = "{name}: {value} " + unit;
}