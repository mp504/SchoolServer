
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Student } from '../../Models/student.model'; // Adjust the path as necessary
import { map,  catchError,} from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class StudentService {
  private apiUrl = 'http://localhost:5188/api/';

  constructor(private http: HttpClient) { }

  getStudentById(id: string): Observable<Student>
  {
    return this.http.get<any>(`${this.apiUrl}Student/${id}`).pipe(
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

  addCourse(studentId: number, courseId: number): Observable<any>
  {
    return this.http.post(`${this.apiUrl}Student/${studentId}/courses`, { courseId });
  }

  deleteCourse(studentId: number, courseId: number): Observable<any>
  {
    return this.http.delete(`${this.apiUrl}Student/${studentId}/courses/${courseId}`);
  }


  getCourses(): Observable<any[]>
  {
   // return this.http.get<any>(`${this.apiUrl}Course`);
    return this.http.get<any>(`${this.apiUrl}Course`).pipe(
      map(response => {
        // Extract the courses array from the response
        const coursesData = response.$values;

        // Map the courses data to extract only the necessary properties
        const courses = coursesData.map((course: any) => ({
          id: course.id,
          courseName: course.courseName
        }));

        return courses;
      })
    );
  }
}
