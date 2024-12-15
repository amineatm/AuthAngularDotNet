import { Component } from '@angular/core';
import { UserComponent } from './user/user.component';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app',
  imports: [RouterOutlet, UserComponent],
  templateUrl: './app.component.html',
  styles: [],
})
export class AppComponent {
  title = 'AuthECClient';
}
