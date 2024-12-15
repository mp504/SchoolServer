
import { Component } from '@angular/core';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-login',
  standalone: false,  // Mark as standalone component
  
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  
  //email: string = '';
  //password: string = '';
  errorMessage: string = '';

  //constructor(private authService: AuthService, private router: Router) { }
  loginForm: FormGroup;
  
  constructor(
    private authService: AuthService,
    private router: Router,
    private formBuilder: FormBuilder
  ) {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }
  onSubmit() {
    if (this.loginForm.valid) {
      const { email, password } = this.loginForm.value;
      this.authService.login(email, password).subscribe(
        (response) => {
          // Save JWT token in localStorage
          localStorage.setItem('token', response.Token);
          localStorage.setItem('Id', response.Id);
          this.router.navigate(['/dashboard']);  // Redirect to the dashboard
        },
        (error) => {
          this.errorMessage = 'Invalid credentials. Please try again.';
        }
      );
      
    }
  }


}
