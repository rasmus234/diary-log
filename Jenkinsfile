pipeline {
  agent any

  environment {
    WEBHOOK_URL = credentials('WEBHOOK_URL')
  }

  stages {
    stage('Build API') {
      when {
        anyOf {
          changeset 'DiaryLog/**'
        }
      }

      steps {
        dir('DiaryLog') {
          sh 'sudo rm -rf ./dist'
          sh 'dotnet build --configuration Release'
          sh 'sudo docker-compose --env-file ../config/test.env build api'
        }
      }
    }

    stage('Build frontend') {
      when {
        changeset 'diary-log-angular/**'
      }

      steps {
        dir('diary-log-angular') {
          sh 'sudo rm -rf ./bin'
          sh 'sudo rm -rf ./obj'
          sh 'sudo ng build'
          sh 'sudo docker-compose --env-file ./config/test.env  build web'
        }
      }
    }

    stage('Unit Tests') {
      steps {
        dir('DiaryLog/DiaryLogApiTests') {
          sh 'sudo rm -rf ./TestResults'
          sh 'dotnet add package coverlet.collector'
          sh 'dotnet test --collect:\'XPlat Code Coverage\''
        }
      }

      post {
        success {
          archiveArtifacts 'DiaryLog/DiaryLogApiTests/TestResults/*/coverage.cobertura.xml'
          publishCoverage adapters: [coberturaAdapter(path: 'DiaryLog/DiaryLogApiTests/TestResults/*/coverage.cobertura.xml', thresholds: [
            [failUnhealthy: true, thresholdTarget: 'Conditional', unhealthyThreshold: 80.0, unstableThreshold: 50.0]
          ])], sourceFileResolver: sourceFiles('NEVER_STORE')
        }
      }
    }

    stage('Clean containers') {
      steps {
        script {
          try {
            sh 'sudo docker-compose --env-file ./config/test.env down'
          } finally {}
        }
      }
    }

    stage('Deploy') {
      steps {
        sh 'sudo docker-compose -p diary-log --env-file ./config/test.env up -d'
      }
    }

    stage('Registry') {
      steps {
        sh 'docker-compose --env-file ./config/test.env push'
      }
    }

    stage('Discord Notification') {
      steps {
        discordSend description: 'Build completed', enableArtifactsList: true, footer: '', image: '', link: '', result: 'SUCCESS', scmWebUrl: 'https://github.com/rasmus234/diary-log', showChangeset: true, thumbnail: '', title: 'Diary Log', webhookURL: WEBHOOK_URL
      }
    }
  }
}