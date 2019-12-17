import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import { ApiGateway } from '../../../api/api.gateway';
import { IProfileData } from '../../../models/recruit.models';
import { SystemEntity } from '../../../models/administraion.models';

@Component({})
export default class CandidateDetailsComponent extends Vue {
    apiGateway = new ApiGateway();
    @Prop()
    candidateId: number | undefined;

    profile: IProfileData = {} as IProfileData;

    menu: boolean = false;
    submitted: boolean = false;
    fetching: boolean = false;
    registrationCompleted: boolean = false;
    backendError: string = "";

    mounted() {
        if (this.candidateId) {
            this.fetchProfile();
        }
    }

    fetchProfile() {
        this.apiGateway.getProfile(this.candidateId as number).then(d => {
            this.profile = d;

        }, err => {
            console.error(err);
        })
    }

    handleSubmit() {
        this.apiGateway.updateProfile(this.profile).then(d => {
            this.profile = d;
        }, err => {
            console.error(err);
        })
    }

    getIdentityCard() {
        alert("GENERATE QR CODE");
    }

    handleDelete() {
        this.apiGateway.deleteCandidate(this.profile.userId).then(d => {
            this.$router.push(`/adminPanel/manage/${SystemEntity.Candidate}`);
        }, err => {
            console.error(err);
        })
    }

    deleteDocument(fileId: number) {
        this.apiGateway.deleteDocument(fileId, this.profile.userId).then(d => {
            this.profile = d.data;
        });
    }

    downloadDocument(fileId: number) {
        if (fileId == this.profile.profilePictureFileId) {
            this.apiGateway.downloadDocument(fileId, this.profile.profilePictureName, this.profile.userId);
            
        } else {
            let doc = this.profile.documents.find(d => d.id == fileId);
            if (doc) {
                this.apiGateway.downloadDocument(fileId, doc.name, this.profile.userId);
            }
        }
    }
}
