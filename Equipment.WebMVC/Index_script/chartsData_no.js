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

    data: ['区科委', '区民政局', '区财政局', '区教育局', '区公安局']
  },
  yAxis: {

    type: 'value',
    boundaryGap: [0, 0.01]
  },
  series: [
    {
      name: '2017年',
      type: 'bar',
      barWidth: '15px',
      data: [10, 9, 8, 7, 6]
    }
  ],
  // backgroundColor: 'rgba(128, 128, 128, 0.5)'
};

smResChart.setOption(smResOption);



//网络情况图表  柱状图

var smNetworkOption = {
  color: ['#3398DB'],
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

    data: ['区科委', '区民政局', '区财政局', '区教育局', '区公安局']
  },
  yAxis: {

    type: 'value',
    boundaryGap: [0, 0.01]
  },
  series: [
    {
      name: '2017年',
      type: 'bar',
      barWidth: '15px',
      data: [10, 9, 8, 7, 6]
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
      name:'机房数量',
      type:'pie',
      radius : '75%',
      center: ['50%', '50%'],
      data:[
        {value:335, name:'区科委'},
        {value:310, name:'区民政局'},
        {value:274, name:'区财政局'},
        {value:235, name:'区教育局'},
        {value:400, name:'区公安局'}
      ].sort(function (a, b) { return a.value - b.value; }),
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

var smTerminalOption =   {
  tooltip: {
    trigger: 'item',
    formatter: "{a} <br/>{b} : {c}%"
  },
  calculable: true,
  series: [
    {
      name:'漏斗图',
      type:'funnel',
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
      data: [
        {value: 60, name: '区检察院'},
        {value: 40, name: '区教育局'},
        {value: 30, name: '区法院'},
        {value: 80, name: '区公安局'},
        {value: 100, name: '区科委'}
      ]
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

var smSafeGuardOption =   {
  tooltip: {
    trigger: 'axis'
  },
  grid: {
    left: '3%',
    right: '4%',
    bottom: '3%',
    top:'2%',
    containLabel: true
  },
  xAxis: {
    type: 'category',
    boundaryGap: false,
    axisLabel: {'interval':0},
    data: ['区科委', '区民政局', '区财政局', '区教育局', '区公安局','区卫计委','区残联']
  },
  yAxis: {
    type: 'value'
  },
  series: [
    {
      name:'邮件营销',
      type:'line',
      stack: '总量',
      data:[120, 132, 101, 134, 90, 230, 210]
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

var smResourcesOption =   {
  tooltip: {
    trigger: 'item',
    formatter: "{a} <br/>{b}: {c} ({d}%)"
  },
  series: [
    {
      name:'资源编目',
      type:'pie',
      radius: ['35%', '65%'],

      data:[
        {value:251, name:'区科委'},
        {value:147, name:'区教育局'},
        {value:202, name:'区财政局'},
        {value:152, name:'区公安局'},
        {value:182, name:'区民政局'},
        {value:102, name:'其他'}
      ]
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
      data : ['区科委', '区民政局', '区财政局', '区教育局', '区公安局','区卫计委','区残联']
    }
  ],
  series : [
    {
      name:'利润',
      type:'bar',
      label: {
        normal: {
          show: true,
          position: 'inside'
        }
      },
      data:[200, 170, 240, 244, 200, 220, 210],
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


