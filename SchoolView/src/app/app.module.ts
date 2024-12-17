import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';  // Import HttpClientModule
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './Auth/login/login.component';
import { StudentDashboardComponent } from './student-dashboard/student-dashboard.component';
import { AuthInterceptor } from '../app//services/auth.interceptor';
import { AuthGuard } from './guards/auth.guard';

const routes: Routes = [
  { path: '', component: AppComponent },
  { path: 'login', component: LoginComponent },
  { path: 'student-dashboard', component: StudentDashboardComponent, canActivate: [AuthGuard] },
  // Add other routes here
  { path: '**', redirectTo: '/login' } // Wildcard route redirects to login
];
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    StudentDashboardComponent,

  ],
  imports: [
    BrowserModule,
    HttpClientModule,  // Add HttpClientModule here
    AppRoutingModule,
    RouterModule.forRoot(routes),
    FormsModule,
    ReactiveFormsModule,
    BrowserModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,  // Use the interceptor
      multi: true
    }],
  bootstrap: [AppComponent]
})

export class AppModule { }
