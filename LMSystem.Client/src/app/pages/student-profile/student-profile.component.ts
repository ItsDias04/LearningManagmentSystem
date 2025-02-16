import { Component, inject } from '@angular/core';
import { Student } from '../../data/interfaces/profile.interface';
import { ProfileService } from '../../data/services/profile.service';


@Component({
  selector: 'app-student-profile',
  imports: [],
  templateUrl: './student-profile.component.html',
  styleUrl: './student-profile.component.css'
})
export class StudentProfileComponent {
  profile: Student | null = null; 
  ProfileService = inject(ProfileService);


  constructor() {
    this.ProfileService.getMeStudent()
    .subscribe((res) => {
      this.profile = res;
    })
  }
}
