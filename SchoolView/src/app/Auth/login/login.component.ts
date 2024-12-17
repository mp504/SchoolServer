
import { Component, OnInit } from '@angular/core';
/*import { CommonModule } from '@angular/common';*/
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';



@Component({
  selector: 'app-login',
  standalone: false,  // Mark as standalone component
  templateUrl: './login.component.html',
  styleUrls:[ './login.component.css']
})
export class LoginComponent implements OnInit{
  
 
  errorMessage: string = '';
  isLoading: boolean = false;

  loginForm: FormGroup;
  
  constructor(
    private authService: AuthService,
    private router: Router,
    private fb: FormBuilder
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }
  onSubmit() {
    if (this.loginForm.valid) {
      this.isLoading = true;
      this.errorMessage = '';
      const { email, password } = this.loginForm.value;
      this.authService.login(email, password).subscribe(
        (response) => {
          // Save JWT token in localStorage

          localStorage.setItem('token', response.token);
          localStorage.setItem('Id', response.id);
         
          this.router.navigate(['student-dashboard']);  // Redirect to the dashboard
        },
        (error) => {
          this.errorMessage = 'Invalid credentials. Please try again.';
          this.isLoading = false;
        }
      );
      
    } else {
      this.loginForm.markAllAsTouched();
    }
  }


}
