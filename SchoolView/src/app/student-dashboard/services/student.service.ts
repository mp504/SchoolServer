
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Student } from '../../Models/student.model'; // Adjust the path as necessary
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class StudentService {
  private apiUrl = 'http://localhost:5188/api/Student';

  constructor(private http: HttpClient) { }

  getStudentById(id: string): Observable<Student>
  {
    return this.http.get<any>(`${this.apiUrl}/${id}`).pipe(
      map(response => {
        // Extract the student data
        const studentData = response.student;
        const coursesData = response.courses.$values;

        // Map the student data into the Student interface
        const student: Student = {
          id: studentData.id,
          firstName: studentData.firstName,
          lastName: studentData.lastName,
          dateOfBirth: new Date(studentData.dateOfBirth), // Convert date string to Date object
          address: studentData.address,
          courses: coursesData // Directly assign the courses array from the response
        };

        return student;
      })
    );
  }

  addCourse(studentId: number, courseId: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/${studentId}/courses`, { courseId });
  }

  deleteCourse(studentId: number, courseId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${studentId}/courses/${courseId}`);
  }
}
