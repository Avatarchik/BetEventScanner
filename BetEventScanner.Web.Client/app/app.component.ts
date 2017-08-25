import { Component } from '@angular/core';

import { HttpService } from './shared/http.service';
import { GithubService } from './shared/github.service';

@Component({
    selector: 'app-root',
    template: `
    <nav class="navbar navbar-fixed-top navbar-inverse">
      <div class="container">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <a class="navbar-brand" href="#">{{title}}</a>
        </div>
        <div id="navbar" class="collapse navbar-collapse">
          <ul class="nav navbar-nav">
            <li><a routerLink="/">Home</a></li>
            <li><a routerLink="event-scan">Event Scan</a></li>
            <li><a routerLink="stats">Stats</a></li>
            <li><a routerLink="profile">Profile</a></li>
          </ul>
        </div><!-- /.nav-collapse -->
      </div><!-- /.container -->
    </nav><!-- /.navbar -->
    <div class="container">
      <router-outlet></router-outlet>    
    </div>
  `,
    styles: [`
body {
  padding-top: 70px;
  background: lightgray;
  font-family: Roboto, sans-serif;
}
`],
    providers: [
        HttpService,
        GithubService
    ]
})

export class AppComponent {
    title = 'Event Scanner App';
}