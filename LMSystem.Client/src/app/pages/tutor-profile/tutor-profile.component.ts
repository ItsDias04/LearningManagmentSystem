import { Component, inject } from '@angular/core';
import { Tutor, Group } from '../../data/interfaces/profile.interface';
import { ProfileService } from '../../data/services/profile.service';


@Component({
  selector: 'app-tutor-profile',
  imports: [],
  templateUrl: './tutor-profile.component.html',
  styleUrl: './tutor-profile.component.css'
})
export class TutorProfileComponent {
  profile: Tutor | null = null; 
  ProfileService = inject(ProfileService);


  constructor() {
    this.ProfileService.getMe()
    .subscribe((res) => {
      this.profile = res;
    })
  }

  getGroupsLength() {
    var groups: string[] = [];
    // var groupsNum = 0;

    this.profile?.subjects.forEach(element => {
      

      element.groups.forEach(elementg => {
        if (groups.findIndex((x) => x == elementg.name) == -1) {
          groups.push(elementg.name);
        }
      });
    });
    return groups.length;
  }
}
