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
var LeagueTableComponent = /** @class */ (function () {
    function LeagueTableComponent() {
        this.teamHover = new core_1.EventEmitter();
    }
    LeagueTableComponent.prototype.isHighlighted = function (name) {
        return this.highlight.indexOf(name) !== -1;
    };
    LeagueTableComponent.prototype.rowEnter = function (name) {
        this.teamHover.emit([name]);
    };
    LeagueTableComponent.prototype.rowLeave = function () {
        this.teamHover.emit([]);
    };
    __decorate([
        core_1.Input(),
        __metadata("design:type", Array)
    ], LeagueTableComponent.prototype, "highlight", void 0);
    __decorate([
        core_1.Input(),
        __metadata("design:type", Object)
    ], LeagueTableComponent.prototype, "table", void 0);
    __decorate([
        core_1.Output(),
        __metadata("design:type", Object)
    ], LeagueTableComponent.prototype, "teamHover", void 0);
    LeagueTableComponent = __decorate([
        core_1.Component({
            selector: 'home-league-table',
            templateUrl: 'league-table.component.html',
            styleUrls: ['league-table.component.scss'],
        })
    ], LeagueTableComponent);
    return LeagueTableComponent;
}());
exports.LeagueTableComponent = LeagueTableComponent;
//# sourceMappingURL=league-table.component.js.map