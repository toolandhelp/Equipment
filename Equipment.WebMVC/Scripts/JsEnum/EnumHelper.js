// JavaScript Document
/*
JavaScript脚本命名法规则如下
(1)变量采用小驼峰法命名法：除第一个单词之外，其他单词首字母大写。
(2)方法采用大驼峰法命名法：相比小驼峰法，大驼峰法把第一个单词的首字母也大写了。
(3)命名采用有效意义命名，尽可能采用全称。
*/
/*javascript中null,undefined,0,"",false作为if的条件的时候，被认为是flase*/
/*javascript除了数字，布尔，字符串这些原始值和null, undefined这些特殊的，其他都是对象*/
/**
 * 旨在解决常规性业务通用功能。
 */
; !function (window, undefined) {
    "use strict";
    window.Enum = {
        version: "1.0.0",
        //文件类型
        FileContentType: function () {
            var stageEnum = {
                /// <summary>
                /// word文档
                /// </summary>
                Word: 1,
                /// <summary>
                /// excel文件
                /// </summary>
                Excel: 2,
                /// <summary>
                /// ppt文件
                /// </summary>
                PPT: 3,
                /// <summary>
                /// 文本文档
                /// </summary>
                TXT: 4,
                /// <summary>
                /// 图片
                /// </summary>
                Image: 5,
                /// <summary>
                /// pdf文件
                /// </summary>
                Pdf: 6,
                /// <summary>
                /// 网页文件
                /// </summary>
                Html: 7,
                /// <summary>
                /// flash文件
                /// </summary>
                SWF: 8,
                /// <summary>
                /// 音频文件
                /// </summary>
                Audio: 9,
                /// <summary>
                /// 视频文件
                /// </summary>
                Video: 10,
                /// <summary>
                /// 其他文件
                /// </summary>
                Other: 99
            };
            return stageEnum;
        },
        //文件类型
        FormatJSONFileContentType: function (s) {
            var stageEnum = this.FileContentType();
            switch (s) {
                case stageEnum.Word: return "Word文档";
                case stageEnum.Excel: return "Excel文件";
                case stageEnum.PPT: return "PPT文件";
                case stageEnum.TXT: return "文本文档";
                case stageEnum.Image: return "图片";
                case stageEnum.Pdf: return "PDF文件";
                case stageEnum.Html: return "网页文件";
                case stageEnum.SWF: return "FLASH文件";
                case stageEnum.Audio: return "音频文件";
                case stageEnum.Video: return "视频文件";
                case stageEnum.Other: return "其他";
                default: return "未知";
            }
        },
        //数据状态
        StageType: function () {
            var stageEnum = {
                /// <summary>
                /// 正常
                /// </summary>
                Normal: 1,
                /// <summary>
                /// 停用
                /// </summary>
                Disable: -1
            };
            return stageEnum;
        },
        //数据状态
        FormatJSONStageType: function (s) {
            var stageEnum = this.StageType();
            switch (s) {
                case stageEnum.Normal: return "正常";
                case stageEnum.Disable: return "停用";
                default: return "未知";
            }
        },
        //考试类型
        SetType: function () {
            var stageEnum = {
                /// <summary>
                /// 单元测试
                /// </summary>
                Unit: 1,
                /// <summary>
                /// 模拟考试
                /// </summary>
                Course: 2
            };
            return stageEnum;
        },
        //考试类型
        FormatJSONSetType: function (s) {
            var stageEnum = this.SetType();
            switch (s) {
                case stageEnum.Unit: return "单元测试";
                case stageEnum.Course: return "模拟考试";
                default: return "未知";
            }
        },
        //题库题目类型
        BankType: function () {
            var stageEnum = {
                /// <summary>
                /// 单选题
                /// </summary>
                Single: 1,
                /// <summary>
                /// 复选题
                /// </summary>
                Multiple: 2,
                /// <summary>
                /// 判断题
                /// </summary>
                Judge: 3,
                /// <summary>
                /// 简答题
                /// </summary>
                Answer: 4
            };
            return stageEnum;
        },
        //题库题目类型
        FormatJSONBankType: function (s) {
            var stageEnum = this.BankType();
            switch (s) {
                case stageEnum.Single: return "单选题";
                case stageEnum.Multiple: return "复选题";
                case stageEnum.Judge: return "判断题";
                case stageEnum.Answer: return "简答题";
                default: return "未知";
            }
        },
        //题库题目难易度
        EasyLevel: function () {
            var stageEnum = {
                /// <summary>
                /// 容易
                /// </summary>
                Easy: 1,
                /// <summary>
                /// 一般
                /// </summary>
                General: 2,
                /// <summary>
                /// 困难
                /// </summary>
                Difficulty: 3
            };
            return stageEnum;
        },
        //题库题目难易度
        FormatJSONEasyLevel: function (s) {
            var stageEnum = this.EasyLevel();
            switch (s) {
                case stageEnum.Easy: return "容易";
                case stageEnum.General: return "一般";
                case stageEnum.Difficulty: return "困难";
                default: return "未知";
            }
        },
        //考试设置考题类型
        EaxmBankType: function () {
            var stageEnum = {
                /// <summary>
                /// 客观题
                /// </summary>
                Objective: 1,
                /// <summary>
                /// 主观题
                /// </summary>
                Subjective: 2,
                /// <summary>
                /// 混合题
                /// </summary>
                Admixture: 3
            };
            return stageEnum;
        },
        //考试设置考题类型
        FormatJSONEaxmBankType: function (s) {
            var stageEnum = this.EaxmBankType();
            switch (s) {
                case stageEnum.Objective: return "客观题";
                case stageEnum.Subjective: return "主观题";
                case stageEnum.Admixture: return "混合题";
                default: return "未知";
            }
        }
    };

    //for 页面模块加载、Node.js运用、页面普通应用
    "function" === typeof define ? define(function () {
        return Enum;
    }) : "undefined" != typeof exports ? module.exports = Enum : window.Enum = Enum;
}(window);
