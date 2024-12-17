
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Retrieve the token from localStorage
    const authToken = localStorage.getItem('token');

    // Clone the request and set the new header in one step.
    if (authToken) {
      const authReq = req.clone({
        setHeaders: {
          Authorization: `Bearer ${authToken}`
        }
      });
      // Pass on the cloned request instead of the original request.
      return next.handle(authReq);
    }

    // If no token, pass the request as is.
    return next.handle(req);
  }
}
