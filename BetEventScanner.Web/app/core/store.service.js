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
var http_1 = require("@angular/http");
var BehaviorSubject_1 = require("rxjs/BehaviorSubject");
require("rxjs/Rx");
var data_actions_1 = require("./actions/data.actions");
var StoreService = /** @class */ (function () {
    function StoreService(http, data) {
        var _this = this;
        this.http = http;
        this.data = data;
        this._dispatching = false;
        this._state = {};
        this._initActions = function () {
            _this.data.init(_this);
        };
        /**
         * Change state.
         *
         * @param reducer
         * @return {StoreState}
         */
        this.dispatch = function (reducer) {
            if (!_this._dispatching) {
                _this._dispatching = true;
                _this._state = reducer(_this._state);
                _this._dispatching = false;
                _this._changes.next(_this._state);
                // console.log('State dispatched', this._state);
                return _this._state;
            }
            else {
                throw new Error('Dispatch in progress');
            }
        };
        /**
         * Full state Observable that emits every dispatch.
         *
         * @return {Observable<T>}
         */
        this.stream = function () {
            return _this._changes.asObservable();
        };
        /**
         * Selected state Observable that emits only selected state dispatches.
         *
         * @param path
         * @param paths
         * @return {Observable<any>}
         */
        this.select = function (path) {
            var paths = [];
            for (var _i = 1; _i < arguments.length; _i++) {
                paths[_i - 1] = arguments[_i];
            }
            return (_a = _this.stream()).pluck.apply(_a, path.split('/').concat(paths)).distinctUntilChanged();
            var _a;
        };
        /**
         * Get Http Headers.
         *
         * @return {Headers}
         */
        this.getHeaders = function () {
            var headers = new http_1.Headers();
            headers.append('X-Auth-Token', '37fd178a35ca427ab0a1d368cf7b9d86');
            return headers;
        };
        /**
         * Get Http.Post Observable.
         *
         * @param url
         * @param data
         * @return {Observable<R>}
         */
        this.post = function (url, data) {
            return _this.http.post(url, JSON.stringify(data), { headers: _this.getHeaders() }).map(function (res) { return res.json(); });
        };
        /**
         * Get Http.Get Observable.
         *
         * @param url
         * @return {Observable<R>}
         */
        this.get = function (url) {
            return _this.http.get(url, { headers: _this.getHeaders() }).map(function (res) { return res.json(); });
        };
        /**
         * Get Http.Delete Observable.
         *
         * @param url
         * @return {Observable<R>}
         */
        this.delete = function (url) {
            return _this.http.delete(url, { headers: _this.getHeaders() }).map(function (res) { return res.json(); });
        };
        this._changes = new BehaviorSubject_1.BehaviorSubject(this._state);
        this._initActions();
    }
    StoreService_1 = StoreService;
    Object.defineProperty(StoreService.prototype, "state", {
        /**
         * Get current state.
         *
         * @return {StoreState}
         */
        get: function () {
            return this._state;
        },
        enumerable: true,
        configurable: true
    });
    /**
     * @param item
     * @return {({}&any)|any}
     * @private
     */
    StoreService.__copyItem = function (item) {
        return Object.assign({}, item);
    };
    /**
     * @param collection
     * @return {any[]}
     * @private
     */
    StoreService.__copyCollection = function (collection) {
        return collection.slice();
    };
    /**
     * @param collection
     * @param item
     * @param key
     * @return {Uint8Array|U[]|Dict<U>|Float32Array|Uint8ClampedArray|Int16Array|any}
     * @private
     */
    StoreService.__setCollectionItem = function (collection, item, key) {
        var _this = this;
        if (key === void 0) { key = 'id'; }
        var updated = false;
        var result = collection.map(function (original) {
            if (original[key] == item[key]) {
                original = _this.__copyItem(item);
                updated = true;
            }
            return original;
        });
        if (!updated) {
            result.push(this.__copyItem(item));
        }
        return result;
    };
    /**
     *
     * @param array
     * @param index
     * @param item
     * @return {any[]}
     * @private
     */
    StoreService.__setArrayItem = function (array, index, item) {
        return array.slice(0, index).concat([
            item
        ], array.slice(index + 1));
    };
    /**
     * @param collection
     * @param keyValue
     * @param key
     * @return {any}
     * @private
     */
    StoreService.__unsetCollectionItem = function (collection, keyValue, key) {
        if (key === void 0) { key = 'id'; }
        return collection.filter(function (item) { return item[key] != keyValue; });
    };
    /**
     * @param left
     * @param right
     * @param key
     * @return {any[]}
     * @private
     */
    StoreService.__mergeCollections = function (left, right, key) {
        if (key === void 0) { key = 'id'; }
        var result = StoreService_1.__copyCollection(left);
        right.forEach(function (item) {
            result = StoreService_1.__setCollectionItem(result, item, key);
        });
        return result;
    };
    /**
     *
     * @param object
     * @param index
     * @param value
     * @return {({}&any&{index: any})|any}
     * @private
     */
    StoreService.__setObjectField = function (object, index, value) {
        var impl = {};
        impl[index] = value;
        return Object.assign({}, object, impl);
    };
    StoreService.addQueryParamsToUrl = function (url, params) {
        var result = url + '?';
        for (var param in params) {
            result += param + '=' + params[param] + '&';
        }
        return result;
    };
    StoreService = StoreService_1 = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.Http,
            data_actions_1.DataActions])
    ], StoreService);
    return StoreService;
    var StoreService_1;
}());
exports.StoreService = StoreService;
//# sourceMappingURL=store.service.js.map