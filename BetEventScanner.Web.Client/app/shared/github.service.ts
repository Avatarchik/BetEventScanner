import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class GithubService {
    private username: string;
    private client_id = '5a4d469043ff14f1493f';
    private client_secret = '99f3e299080ec97391cc00903df3792d843f9b13';

    constructor(private http: Http) {
        console.log('Github Service Ready ...');
        this.username = 'JoSaint13';
    }

    getUser() {
        // tslint:disable-next-line:max-line-length
        return this.http.get('http://api.github.com/users/' + this.username + '?client_id=' + this.client_id + '&client_secret=' + this.client_secret)
            .map(res => res.json());
    }

    getRepos() {
        // tslint:disable-next-line:max-line-length
        return this.http.get('http://api.github.com/users/' + this.username + '/repos?client_id=' + this.client_id + '&client_secret=' + this.client_secret)
            .map(res => res.json());
    }
}
