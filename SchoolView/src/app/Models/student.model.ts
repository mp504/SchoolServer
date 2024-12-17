// File: src/app/models/student.model.ts

import { Address } from '../Models/address.model';
import { Course } from '../Models/course.model';

 export interface Student {
  id: number;
   firstName: string;
  lastName: string;
   dateOfBirth: Date;
  address: Address;
  courses: Course[];
}
