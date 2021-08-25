am4core.useTheme(am4themes_animated);

// create chart
var chart = am4core.create("chartdiv", am4charts.TreeMap);

/*
SELECT 
TiposPostais.Sigla, 
COUNT(TiposPostais.Sigla) AS Qtd
FROM TabelaObjetosSROLocal INNER JOIN TiposPostais 
ON Mid(TabelaObjetosSROLocal.CodigoObjeto, 1, 2) = TiposPostais.Sigla 
WHERE TiposPostais.TipoClassificacao = 'PAC'
GROUP BY TiposPostais.Sigla
ORDER BY 2
*/

//PC	6
chart.data = [{
	name: "PAC",
	children: 
	[
		{ name: "PC", value: 6 }
        , { name: "PC", value: 6 }
	]
},
{
	name: "SEDEX",
	children: 
	[
		{ name: "DN", value: 1}
	]
},
{
	name: "DIVERSOS",
	children: 
	[
		{ name: "EA", value: 1   }
	]
}];

chart.colors.step = 2;

// define data fields
chart.dataFields.value = "value";
chart.dataFields.name = "name";
chart.dataFields.children = "children";

chart.zoomable = false;

// level 0 series template
var level0SeriesTemplate = chart.seriesTemplates.create("0");
var level0ColumnTemplate = level0SeriesTemplate.columns.template;

level0ColumnTemplate.column.cornerRadius(10, 10, 10, 10);
level0ColumnTemplate.fillOpacity = 0;
level0ColumnTemplate.strokeWidth = 4;
level0ColumnTemplate.strokeOpacity = 0;

// level 1 series template
var level1SeriesTemplate = chart.seriesTemplates.create("1");
var level1ColumnTemplate = level1SeriesTemplate.columns.template;

level1SeriesTemplate.tooltip.animationDuration = 0;
level1SeriesTemplate.strokeOpacity = 1;

level1ColumnTemplate.column.cornerRadius(10, 10, 10, 10)
level1ColumnTemplate.fillOpacity = 1;
level1ColumnTemplate.strokeWidth = 4;
level1ColumnTemplate.stroke = am4core.color("#ffffff");

var bullet1 = level1SeriesTemplate.bullets.push(new am4charts.LabelBullet());
bullet1.locationY = 0.5;
bullet1.locationX = 0.5;
bullet1.label.text = "{name}";
bullet1.label.fill = am4core.color("#ffffff");

chart.maxLevels = 2;