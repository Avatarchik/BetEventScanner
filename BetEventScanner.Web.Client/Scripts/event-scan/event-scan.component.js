"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var forms_1 = require('@angular/forms');
var EventScanComponent = (function () {
    function EventScanComponent(_fb) {
        this._fb = _fb;
        this.events = ['win', 'draw', 'loss'];
        this.selectForm = this._fb.group({
            results: this.buildArray()
        });
    }
    EventScanComponent.prototype.buildArray = function () {
        this.results = this._fb.array([
            this.buildGroup()
        ]);
        return this.results;
    };
    EventScanComponent.prototype.buildGroup = function () {
        return this._fb.group({
            result: ''
        });
    };
    EventScanComponent.prototype.add = function () {
        this.results.push(this.buildGroup());
    };
    EventScanComponent.prototype.onSubmit = function () {
        console.log(this.selectForm.value);
    };
    EventScanComponent = __decorate([
        core_1.Component({
            selector: 'app-event-scan',
            template: "\n<div class=\"panel panel-primary\">\n    <div class=\"panel-heading\">\n        <h3 class=\"panel-title\">Form Select</h3>\n    </div>\n    <div class=\"panel-body\">\n        <form novalidate [formGroup]=\"selectForm\" (ngSubmit)='onSubmit()'>\n            <div formArrayName=\"results\">\n                <a class=\"btn btn-success\" (click)=\"add()\">\n                    Add\n                </a>\n\n                <div class=\"list-group\" *ngFor=\"let result of results.controls; let i=index\" [formGroupName]=\"i\">\n                    <span class=\"list-group-item\">\n                        Choose the result of match \u2116{{i+1}}:\n                           <label class=\"radio-inline\">\n                              <input type=\"radio\" class=\"form-control\" value=\"winning\" formControlName=\"result\"> W\n                           </label>\n\n                           <label class=\"radio-inline\">\n                              <input type=\"radio\" class=\"form-control\" value=\"drawing\" formControlName=\"result\"> D\n                           </label>\n\n                           <label class=\"radio-inline\">\n                              <input type=\"radio\" class=\"form-control\" value=\"losing\" formControlName=\"result\"> L\n                           </label>\n                    </span>\n                </div>\n            </div>\n            <div class=\"panel-footer\">\n                <button class=\"btn btn-primary\" type=\"submit\">Send Data</button>\n            </div>\n        </form>\n    </div>\n</div>\n<div class=\"panel panel-warning\">\n    <div class=\"panel-heading\">\n        <h3 class=\"panel-title\">Form Request</h3>\n    </div>\n    <div class=\"panel-body\">\n        <pre>{{ selectForm.value | json }}</pre>\n    </div>\n</div> \n",
            styles: ["\n.form-control {\n  height: auto;\n  width: auto;\n}\n\n.list-group {\n  margin-bottom: 0;\n}\n\n.radio-inline {\n  margin-left: 30px;\n}\n"]
        }), 
        __metadata('design:paramtypes', [forms_1.FormBuilder])
    ], EventScanComponent);
    return EventScanComponent;
}());
exports.EventScanComponent = EventScanComponent;
//# sourceMappingURL=event-scan.component.js.map