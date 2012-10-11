//默认工单地址
var TestOrderaddress = document.getElementById("HiddenField_testOrderAdress");
//alert(TestOrderaddress.value);
//默认公司经纬度中心点坐标
var companyLongitude = document.getElementById("HiddenField_Longitude").value;
var companyLatitude = document.getElementById("HiddenField_Latitude").value;
//alert(companyLatitude);
// 地图状态
google.load('search', '1');
google.load('maps', '2');
// 地图状态字段声明
var gLocalSearch;
var gMap;
var gSelectedResults = [];
var gCurrentResults = [];
var gSearchForm;

// 创建“微型”标记图标
var gSmallIcon = null;


// 设置地图和搜索的属性
function OnLoad() {
    gSmallIcon = new google.maps.Icon();
    gSmallIcon.image = "http://labs.google.com/ridefinder/images/mm_20_yellow.png";
    gSmallIcon.shadow = "http://labs.google.com/ridefinder/images/mm_20_shadow.png";
    gSmallIcon.iconSize = new google.maps.Size(12, 20);
    gSmallIcon.shadowSize = new google.maps.Size(22, 20);
    gSmallIcon.iconAnchor = new google.maps.Point(6, 20);
    gSmallIcon.infoWindowAnchor = new google.maps.Point(5, 1);

    gSearchForm = new google.search.SearchForm(false, document.getElementById("searchform"));
    gSearchForm.setOnSubmitCallback(null, CaptureForm);
    gSearchForm.input.focus();

    //初始化地图设置中心点
    gMap = new google.maps.Map2(document.getElementById("map"));
    //gMap.addControl(new GOverviewMapControl());小视图
    gMap.setUIToDefault(); //设置默认UI界面
    //gMap.setCenter(new google.maps.LatLng(companyLongitude, companyLatitude), 16); //中心点和视图放大标度1-17
    gMap.setCenter(new google.maps.LatLng(24.467571, 118.113155), 12);

    // 初始化本地搜索
    gLocalSearch = new google.search.LocalSearch();
    gLocalSearch.setCenterPoint(gMap);
    gLocalSearch.setSearchCompleteCallback(null, OnLocalSearch);

    //执行默认搜索
    gSearchForm.execute(TestOrderaddress.value);
}

// 返回本地搜索集，清理旧数据
// results and load the new ones.
function OnLocalSearch() {
    if (!gLocalSearch.results) return;
    var searchWell = document.getElementById("searchwell");

    // Clear the map and the old search well
    searchWell.innerHTML = "";
    for (var i = 0; i < gCurrentResults.length; i++) {
        if (!gCurrentResults[i].selected()) {
            gMap.removeOverlay(gCurrentResults[i].marker());
        }
    }

    gCurrentResults = [];
    for (var i = 0; i < gLocalSearch.results.length; i++) {
        gCurrentResults.push(new LocalResult(gLocalSearch.results[i]));
    }

    var attribution = gLocalSearch.getAttribution();
    if (attribution) {
        document.getElementById("searchwell").appendChild(attribution);
    }

    // move the map to the first result
    var first = gLocalSearch.results[0];
    gMap.panTo(new google.maps.LatLng(first.lat, first.lng));
}

// Cancel the form submission, executing an AJAX Search API search.
function CaptureForm(searchForm) {
    gLocalSearch.execute(searchForm.input.value);
    return false;
}



// A class representing a single Local Search result returned by the
// Google AJAX Search API.
function LocalResult(result) {
    this.result_ = result;
    this.resultNode_ = this.unselectedHtml();
    document.getElementById("searchwell").appendChild(this.resultNode_);
    gMap.addOverlay(this.marker(gSmallIcon));
}

// Returns the GMap marker for this result, creating it with the given
// icon if it has not already been created.
LocalResult.prototype.marker = function (opt_icon) {
    if (this.marker_) return this.marker_;
    var marker = new google.maps.Marker(new google.maps.LatLng(parseFloat(this.result_.lat),
                                             parseFloat(this.result_.lng)),
                                   opt_icon);
    GEvent.bind(marker, "click", this, function () {
        marker.openInfoWindow(this.selected() ? this.selectedHtml() :
                                                    this.unselectedHtml());
    });
    this.marker_ = marker;
    return marker;
}

// "Saves" this result if it has not already been saved
LocalResult.prototype.select = function () {
    if (!this.selected()) {
        this.selected_ = true;

        // 移除旧标记，增加新标记
        gMap.removeOverlay(this.marker());
        this.marker_ = null;
        gMap.addOverlay(this.marker(G_DEFAULT_ICON));

        // 保存我们选定的路线
        document.getElementById("selected").appendChild(this.selectedHtml());

        // 移除我们所选定的路线
        this.resultNode_.parentNode.removeChild(this.resultNode_);
    }
}

// Returns the HTML we display for a result before it has been "saved"
LocalResult.prototype.unselectedHtml = function () {
    var container = document.createElement("div");
    container.className = "unselected";
    container.appendChild(this.result_.html.cloneNode(true));
    var saveDiv = document.createElement("div");
    saveDiv.className = "select";
    saveDiv.innerHTML = "Save this location";
    GEvent.bindDom(saveDiv, "click", this, function () {
        gMap.closeInfoWindow();
        this.select();
        gSelectedResults.push(this);
    });
    container.appendChild(saveDiv);
    return container;
}

// Returns the HTML we display for a result after it has been "saved"
LocalResult.prototype.selectedHtml = function () {
    return this.result_.html.cloneNode(true);
}

// Returns true if this result is currently "saved"
LocalResult.prototype.selected = function () {
    return this.selected_;
}

google.setOnLoadCallback(OnLoad, true);
//alert(companyLatitude);
//]]>