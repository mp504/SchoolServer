import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { StudentService } from './services/student.service';
import { Student } from '../Models/student.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-student-dashboard',
  standalone: false,
  
  templateUrl: './student-dashboard.component.html',
  styleUrl: './student-dashboard.component.css'
})
export class StudentDashboardComponent {
  student: Student | null = null;
  addCourseForm: FormGroup;
  Id: (string| null)  = localStorage.getItem('Id') ;
  constructor(private studentService: StudentService, private fb: FormBuilder, private router: Router) {
    this.addCourseForm = this.fb.group({
      studentId: [null, Validators.required],
      courseId: [null, Validators.required]
    });
  }


  ngOnInit(): void {
    this.fetchStudentData();
  }

  fetchStudentData(): void {
    console.log(this.Id);
    if (this.Id) {
      this.studentService.getStudentById(this.Id).subscribe(
        (student) => {
          this.student = student;
          console.log(this.student.id);
          console.log(this.student.firstName);
          this.addCourseForm.patchValue({ studentId: this.student.id });
        },
        (error) => {
          console.error('Error fetching student data:', error);
          this.router.navigate(['/login']);
        }
      );
    } else {
      console.error('No student ID found in localStorage.');
      this.router.navigate(['/login']);
    }
  }
  addCourse(): void {
    if (this.addCourseForm.valid && this.student) {
      const { courseId } = this.addCourseForm.value;
      this.studentService.addCourse(this.student.id, courseId).subscribe(
        () => {
          this.fetchStudentData();
        },
        (error) => {
          console.error('Error adding course:', error);
        }
      );
    } else {
      this.addCourseForm.markAllAsTouched();
    }
  }

  deleteCourse(studentId: number, courseId: number): void {
    if (this.student && confirm('Are you sure you want to delete this course?')) {
      this.studentService.deleteCourse(studentId, courseId).subscribe(
        () => {
          this.fetchStudentData();
        },
        (error) => {
          console.error('Error deleting course:', error);
        }
      );
    }
  }


}
