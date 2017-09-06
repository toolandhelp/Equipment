//建设项目
var jsAxisData = [], jsseriesData = [];

$.ajax({
    url: 'JSApi',
    type: 'POST',
    dataType: "json",
    async: false,
    beforeSend: function () { $("#sm-res").html("<div style='width:120px;margin: 0 auto;'><img src='/Images/loading.gif' width='32px' height='32px' align='absmiddle' /> 正在加载．．．</div>"); },
    ContentType: "application/json;charset=utf-8",
    success: function (responseList) {
        if (responseList.ErrorType == "1") {
            for (var i = 0; i < responseList.Entity.length; i++) {
                jsAxisData.push(responseList.Entity[i].name);
                jsseriesData.push(responseList.Entity[i].value);
            }
        }
    },
    error: function (xmlHttpRequest, textStatus, errorThrown) {
        MISSY.iDebugAjaxError(xmlHttpRequest, textStatus, errorThrown);
        $("#sm-res").html("数据加载失败,请稍后再试！");
    }
});

var smResChart = echarts.init(document.getElementById('sm-res'), {
  renderer: 'canvas',
  width: 'auto',
  height: 'auto'
});
var smResOption = {

  tooltip: {
    trigger: 'axis',
    axisPointer: {
      type: 'shadow'
    }
  },
  // formatter:'{c}%',
//      legend: {
//        data: ['2017年']
//      },
  grid: {
    top: '10%',
    left: '3%',
    right: '4%',
    bottom: '3%',
    containLabel: true
  },
  xAxis: {
    type: 'category',
    axisLabel: {'interval':0},

    data: jsAxisData
  },
  yAxis: {

    type: 'value',
    boundaryGap: [0, 0.01]
  },
  series: [
    {
      name: '项目预算',
      type: 'bar',
      barWidth: '15px',
      data: jsseriesData
    }
  ],
  // backgroundColor: 'rgba(128, 128, 128, 0.5)'
};

smResChart.setOption(smResOption);



//网络情况图表  柱状图

var wlAxisData = [], wlseriesData = [];

$.ajax({
    url: 'WlApi',
    type: 'POST',
    dataType: "json",
    async: false,
    beforeSend: function () { $("#sm-network").html("<div style='width:120px;margin: 0 auto;'><img src='/Images/loading.gif' width='32px' height='32px' align='absmiddle' /> 正在加载．．．</div>"); },
    ContentType: "application/json;charset=utf-8",
    success: function (responseList) {
        if (responseList.ErrorType == "1") {
            for (var i = 0; i < responseList.Entity.length; i++) {
                wlAxisData.push(responseList.Entity[i].wlAxisData);
                wlseriesData.push(responseList.Entity[i].wlseriesData);
            }
        }
    },
    error: function (xmlHttpRequest, textStatus, errorThrown) {
        MISSY.iDebugAjaxError(xmlHttpRequest, textStatus, errorThrown);
        $("#sm-network").html("数据加载失败,请稍后再试！");
    }
});

var smNetworkOption = {
  color: ['#3398DB'],
  tooltip: {
    trigger: 'axis',
    axisPointer: {
      type: 'shadow'
    }
  },
  grid: {
    top: '10%',
    left: '3%',
    right: '4%',
    bottom: '3%',
    containLabel: true
  },
  xAxis: {
    type: 'category',
    axisLabel: {'interval':0},
    data: wlAxisData
  },
  yAxis: {
    type: 'value',
    boundaryGap: [0, 0.01]
  },
  series: [
    {
      name: '光纤',
      type: 'bar',
      barWidth: '15px',
      data: wlseriesData
    }
  ],
};


var smNetworkChart = echarts.init(document.getElementById('sm-network'), {
  renderer: 'canvas',
  width: 'auto',
  height: 'auto'
});

smNetworkChart.setOption(smNetworkOption);



// 机房情况 自定义饼图

var  jfseriesData = [];

$.ajax({
    url: 'JFApi',
    type: 'POST',
    dataType: "json",
    async: false,
    beforeSend: function () { $("#sm-room").html("<div style='width:120px;margin: 0 auto;'><img src='/Images/loading.gif' width='32px' height='32px' align='absmiddle' /> 正在加载．．．</div>"); },
    ContentType: "application/json;charset=utf-8",
    success: function (responseList) {
        if (responseList.ErrorType == "1") {
            jfseriesData = responseList.Entity;
            //for (var i = 0; i < responseList.Entity.length; i++) {
            //    jfseriesData.push(responseList.Entity[i].wlAxisData);
            //    wlseriesData.push(responseList.Entity[i].wlseriesData);
            //}
        }
    },
    error: function (xmlHttpRequest, textStatus, errorThrown) {
        MISSY.iDebugAjaxError(xmlHttpRequest, textStatus, errorThrown);
        $("#sm-room").html("数据加载失败,请稍后再试！");
    }
});


var smRoomOption =  {
  // backgroundColor: '#2c343c',

  // title: {
  //   text: 'Customized Pie',
  //   left: 'center',
  //   top: 20,
  //   textStyle: {
  //     color: '#ccc'
  //   }
  // },

  tooltip : {
    trigger: 'item',
    formatter: "{a} <br/>{b} : {c} ({d}%)"
  },
  visualMap: {
    show: false,
    min: 80,
    max: 600,
    inRange: {
      colorLightness: [0, 1]
    }
  },
  series : [
    {
      name:'服务器数量',
      type:'pie',
      radius : '75%',
      center: ['50%', '50%'],
      data: jfseriesData.sort(function (a, b) { return a.value - b.value; }),
      roseType: 'radius',
      label: {
        normal: {
          textStyle: {
            color: '#fff'
          }
        }
      },
      labelLine: {
        normal: {
          lineStyle: {
            color: '#fff'
          },
          smooth: 0.2,
          length: 10,
          length2: 20
        }
      },
      itemStyle: {
        normal: {
          color: '#c23531',
          shadowBlur: 200,
          shadowColor: 'rgba(0, 0, 0, 0.9)'
        }
      },

      animationType: 'scale',
      animationEasing: 'elasticOut',
      animationDelay: function (idx) {
        return Math.random() * 200;
      }
    }
  ]
};


var smRoomChart = echarts.init(document.getElementById('sm-room'), {
  renderer: 'canvas',
  width: 'auto',
  height: 'auto'
});

smRoomChart.setOption(smRoomOption);

// 终端情况 雷达图


var zdseriesData = [];

$.ajax({
    url: 'ZDApi',
    type: 'POST',
    dataType: "json",
    async: false,
    beforeSend: function () { $("#sm-terminal").html("<div style='width:120px;margin: 0 auto;'><img src='/Images/loading.gif' width='32px' height='32px' align='absmiddle' /> 正在加载．．．</div>"); },
    ContentType: "application/json;charset=utf-8",
    success: function (responseList) {
        if (responseList.ErrorType == "1") {
            zdseriesData = responseList.Entity;
            //for (var i = 0; i < responseList.Entity.length; i++) {
            //    jfseriesData.push(responseList.Entity[i].wlAxisData);
            //    wlseriesData.push(responseList.Entity[i].wlseriesData);
            //}
        }
    },
    error: function (xmlHttpRequest, textStatus, errorThrown) {
        MISSY.iDebugAjaxError(xmlHttpRequest, textStatus, errorThrown);
        $("#sm-terminal").html("数据加载失败,请稍后再试！");
    }
});



var smTerminalOption =   {
  tooltip: {
    trigger: 'item',
    formatter: "{a} <br/>{b} : {c}(台)"
  },
  calculable: true,
  series: [
      {
          name: '漏斗图',
          type: 'funnel',
          left: '10%',
          top: 10,
          //x2: 80,
          bottom: 0,
          width: '80%',
          // height: {totalHeight} - y - y2,
          min: 0,
          max: 100,
          minSize: '0%',
          maxSize: '100%',
          sort: 'descending',
          gap: 2,
          label: {
              normal: {
                  show: true,
                  position: 'inside'
              },
              emphasis: {
                  textStyle: {
                      fontSize: 20
                  }
              }
          },
          labelLine: {
              normal: {
                  length: 10,
                  lineStyle: {
                      width: 1,
                      type: 'solid'
                  }
              }
          },
          itemStyle: {
              normal: {
                  borderColor: '#fff',
                  borderWidth: 1
              }
          },
          data: zdseriesData
      }
  ]
};



var smTerminalChart = echarts.init(document.getElementById('sm-terminal'), {
  renderer: 'canvas',
  width: 'auto',
  height: 'auto'
});

smTerminalChart.setOption(smTerminalOption);



// 安全设备 折线图

var aqseriesData = [], aqAxisData = [];

$.ajax({
    url: 'AQApi',
    type: 'POST',
    dataType: "json",
    async: false,
    beforeSend: function () { $("#sm-safeguard").html("<div style='width:120px;margin: 0 auto;'><img src='/Images/loading.gif' width='32px' height='32px' align='absmiddle' /> 正在加载．．．</div>"); },
    ContentType: "application/json;charset=utf-8",
    success: function (responseList) {
        if (responseList.ErrorType == "1") {
            //aqseriesData = responseList.Entity;
            for (var i = 0; i < responseList.Entity.length; i++) {
                aqAxisData.push(responseList.Entity[i].name);
                aqseriesData.push(responseList.Entity[i].value);
            }
        }
    },
    error: function (xmlHttpRequest, textStatus, errorThrown) {
        MISSY.iDebugAjaxError(xmlHttpRequest, textStatus, errorThrown);
        $("#sm-safeguard").html("数据加载失败,请稍后再试！");
    }
});


var smSafeGuardOption = {
    tooltip: {
        trigger: 'axis'
    },
    grid: {
        left: '3%',
        right: '4%',
        bottom: '3%',
        top: '2%',
        containLabel: true
    },
    xAxis: {
        type: 'category',
        boundaryGap: false,
        axisLabel: { 'interval': 0 },
        data: aqAxisData
    },
    yAxis: {
        type: 'value'
    },
    series: [
        {
            name: '邮件审计设备',
            type: 'line',
            stack: '总量',
            data: aqseriesData
        }
    ]
};


var smSafeGuardChart = echarts.init(document.getElementById('sm-safeguard'), {
  renderer: 'canvas',
  width: 'auto',
  height: 'auto'
});

smSafeGuardChart.setOption(smSafeGuardOption);



// 资源编目 饼图环形


var zyseriesData = [];

$.ajax({
    url: 'ZYApi',
    type: 'POST',
    dataType: "json",
    async: false,
    beforeSend: function () { $("#sm-resources").html("<div style='width:120px;margin: 0 auto;'><img src='/Images/loading.gif' width='32px' height='32px' align='absmiddle' /> 正在加载．．．</div>"); },
    ContentType: "application/json;charset=utf-8",
    success: function (responseList) {
        if (responseList.ErrorType == "1") {
            zyseriesData = responseList.Entity;
            //for (var i = 0; i < responseList.Entity.length; i++) {
            //    jfseriesData.push(responseList.Entity[i].wlAxisData);
            //    wlseriesData.push(responseList.Entity[i].wlseriesData);
            //}
        }
    },
    error: function (xmlHttpRequest, textStatus, errorThrown) {
        MISSY.iDebugAjaxError(xmlHttpRequest, textStatus, errorThrown);
        $("#sm-resources").html("数据加载失败,请稍后再试！");
    }
});


var smResourcesOption =   {
  tooltip: {
    trigger: 'item',
    formatter: "{a} <br/>{b}: {c} ({d}%)"
  },
  series: [
    {
      name:'资源编目数量',
      type:'pie',
      radius: ['35%', '65%'],

      data: zyseriesData
    }
  ]
};


var smResourcesChart = echarts.init(document.getElementById('sm-resources'), {
  renderer: 'canvas',
  width: 'auto',
  height: 'auto'
});

smResourcesChart.setOption(smResourcesOption);



// 正版软件 柱状图



var zbseriesData = [], zbAxisData = [];

$.ajax({
    url: 'ZBApi',
    type: 'POST',
    dataType: "json",
    async: false,
    beforeSend: function () { $("#sm-software").html("<div style='width:120px;margin: 0 auto;'><img src='/Images/loading.gif' width='32px' height='32px' align='absmiddle' /> 正在加载．．．</div>"); },
    ContentType: "application/json;charset=utf-8",
    success: function (responseList) {
        if (responseList.ErrorType == "1") {
            //aqseriesData = responseList.Entity;
            for (var i = 0; i < responseList.Entity.length; i++) {
                zbAxisData.push(responseList.Entity[i].name);
                zbseriesData.push(responseList.Entity[i].value);
            }
        }
    },
    error: function (xmlHttpRequest, textStatus, errorThrown) {
        MISSY.iDebugAjaxError(xmlHttpRequest, textStatus, errorThrown);
        $("#sm-software").html("数据加载失败,请稍后再试！");
    }
});


var smSoftwareOption =   {
  tooltip : {
    trigger: 'axis',
    axisPointer : {            // 坐标轴指示器，坐标轴触发有效
      type : 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
    }
  },
  grid: {
    left: '3%',
    right: '4%',
    bottom: '3%',
    top:'1%',
    containLabel: true
  },
  xAxis : [
    {
      type : 'value'
    }
  ],
  yAxis : [
    {
      type : 'category',
      axisTick : {show: false},
      data: zbAxisData
    }
  ],
  series : [
    {
      name:'操作系统数量',
      type:'bar',
      label: {
        normal: {
          show: true,
          position: 'inside'
        }
      },
      data: zbseriesData,
      barWidth: '8px'
    }
  ]
};



var smSoftwareChart = echarts.init(document.getElementById('sm-software'), {
  renderer: 'canvas',
  width: 'auto',
  height: 'auto'
});

smSoftwareChart.setOption(smSoftwareOption);


