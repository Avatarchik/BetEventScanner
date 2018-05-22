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
var rxjs_1 = require("rxjs");
var store_service_1 = require("../core/store.service");
var FetchFixturesResolver = /** @class */ (function () {
    function FetchFixturesResolver(store) {
        this.store = store;
    }
    FetchFixturesResolver.prototype.resolve = function (route, state) {
        var _this = this;
        return new rxjs_1.Observable(function (observer) {
            var competition = _this.store.state.data.competitions.find(function (competition) { return competition.league == route.params['league']; });
            if (competition.fixtures.length > 0) {
                observer.next(true);
                observer.complete();
            }
            else {
                _this.store.data.fetchFixtures(competition.id, competition.currentMatchday).subscribe(function (state) {
                    observer.next(true);
                    observer.complete();
                });
            }
        });
    };
    FetchFixturesResolver = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [store_service_1.StoreService])
    ], FetchFixturesResolver);
    return FetchFixturesResolver;
}());
exports.FetchFixturesResolver = FetchFixturesResolver;
//# sourceMappingURL=fetch-fixtures.resolver.js.map