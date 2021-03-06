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
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var store_service_1 = require("../../core/store.service");
var HomePageComponent = /** @class */ (function () {
    function HomePageComponent(store) {
        this.store = store;
        console.log('store', this.store.state);
    }
    HomePageComponent.prototype.ngOnInit = function () {
        this.store.data.fetchCompetitions().subscribe(function (state) {
            console.log('state', state);
        });
    };
    HomePageComponent = __decorate([
        core_1.Component({
            selector: 'home-home-page',
            templateUrl: 'home-page.component.html',
            styleUrls: ['home-page.component.scss'],
        }),
        __metadata("design:paramtypes", [store_service_1.StoreService])
    ], HomePageComponent);
    return HomePageComponent;
}());
exports.HomePageComponent = HomePageComponent;
//# sourceMappingURL=home-page.component.js.map