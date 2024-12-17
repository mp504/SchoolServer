import { Component } from '@angular/core';
import { Router } from '@angular/router';
/*import { LoginComponent } from './Auth/login/login.component';*/
import { FormBuilder, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrls: ['./app.component.css']
})
export class AppComponent
{

  constructor(private router: Router) { }

  navigateToLogin() {
    this.router.navigate(['/login']);

  }


  navigateToDashboard() {
    this.router.navigate(['/dashboard']);
  }
}
